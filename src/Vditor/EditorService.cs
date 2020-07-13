using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Vditor
{
    public class EditorService
    {
        private readonly IJSRuntime _js;

        public EditorService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task CreateVditor(ElementReference @ref, Editor editor)
        {
            await _js.InvokeVoidAsync("createVditor", @ref, DotNetObjectReference.Create(editor), editor.Value);
        }

        public async ValueTask<string> GetValue(ElementReference @ref)
        {
            return await _js.InvokeAsync<string>("getValue", @ref);
        }

        public async Task SetValue(ElementReference @ref, string value)
        {
            await _js.InvokeVoidAsync("setValue", @ref, value);
        }
    }
}