using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vditor.Docs.Server {
    [Route("api")]
    [ApiController]
    public class ValuesController : System.Web.Http.ApiController {
        [HttpPost("upload")]
        public async Task<IActionResult> PostUrl() {
            if (Request.Content.IsMimeMultipartContent()) {
                string root = System.IO.Path.Combine(Environment.CurrentDirectory, "wwwroot"); //<==Change Content Path
                var provider = new MultipartFormDataStreamProvider(root);
                await Request.Content.ReadAsMultipartAsync(provider);
                Dictionary<string, string> dict = new Dictionary<string, string>(); //<==Use A dictionary To differ pictures in vditor upload
                foreach (var file in provider.FileData) {
                    string filename = file.Headers.ContentDisposition.FileName.Trim('"');
                    System.IO.FileInfo info = new System.IO.FileInfo(file.LocalFileName);
                    string savepath = System.IO.Path.Combine(root, filename);
                    if (!System.IO.File.Exists(savepath)) { //<== Skip File If Exists
                        info.MoveTo(savepath);
                    }
                    dict.Add(filename, "api/image/" + filename);
                }
                return new JsonResult(new {
                    Msg = "",
                    Code = 0,
                    Data = new { SuccMap = dict }
                });
            }

            return new JsonResult(new {
                Msg = "错误", Code = -1000, Data = new { }
            });
        }

        [HttpGet("image/{id}")]
        public IActionResult GetImage(string id) {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "wwwroot", id);
            if (System.IO.File.Exists(path)) {
                return new PhysicalFileResult(path, "image/png"); //<==Change Media Content Type
            } else {
                return NotFound();
            }
        }
    }
}
