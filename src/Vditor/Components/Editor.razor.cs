using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Vditor
{
    public partial class Editor : ComponentBase
    {
        [Inject] private IJSRuntime JS { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JS.InvokeVoidAsync("vditorScript");
            }
        }
    }
}