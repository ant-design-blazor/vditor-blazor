namespace Vditor.Models
{
    /// <summary>
    /// 普通参数在此定义
    /// 函数在Editor.razor.Upload.cs中定义(没写)
    /// </summary>
    /// <remarks>
    /// Options.Upload 's parameters are defined here.
    /// All callback functions are defined in Editor.razor.Upload.cs. (No implement by far yet.)
    /// </remarks>
    /// <see cref="https://ld246.com/article/1549638745630#options-upload"/>
    public class Upload
    {
        public string Url { get; set; }
        public int Max { get; set; } = 10 * 1024 * 1024;
        public string LinkToImgUrl { get; set; }
        public string Token { get; set; }
        public bool WithCredentials { get; set; } = false;
        public string Accept { get; set; }
        public bool Multiple { get; set; } = true;
        public string FieldName { get; set; } = "file[]";
        public string ExtraData { get; set; } = "";
    }
}