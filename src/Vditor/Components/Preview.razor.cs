using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vditor
{
    public partial class Preview : ComponentBase
    {
        [Parameter]
        public string Markdown
        {
            get => markdown;
            set
            {
                if (markdown != value)
                {
                    markdown = value;
                    waitingUpdate = true;
                }
            }
        }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Options { get; set; }

        [Parameter]
        public bool Anchor { get; set; }

        [Parameter]
        public bool Lazyload { get; set; }

        [Parameter]
        public string LazyloadImage { get; set; } = defaultLazyloadImage;

        [Parameter]
        public EventCallback OnRendered { get; set; }

        [Inject] private IJSRuntime Js { get; set; }

        private ElementReference _ref;
        private string markdown;
        private bool waitingUpdate;
        private bool afterFisrtRender;

        private const string defaultLazyloadImage = "https://cdn.jsdelivr.net/npm/vditor/dist/images/img-loading.svg";

        protected override void OnInitialized()
        {
            Options ??= new Dictionary<string, object>();
            Options["Anchor"] = Anchor ? (int?)1 : null;
            Options["LazyLoadImage"] = Lazyload ? LazyloadImage : null;
            Options["HandleAfter"] = OnRendered.HasDelegate;
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                this.afterFisrtRender = true;
            }

            if (afterFisrtRender && waitingUpdate)
            {
                waitingUpdate = false;
                await SetPreview();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task SetPreview()
        {
            await Js.InvokeVoidAsync("vditorBlazor.preview", _ref, DotNetObjectReference.Create(this), this.Markdown, this.Options);
        }

        [JSInvokable]
        public async Task HandleAfter()
        {
            if (OnRendered.HasDelegate)
            {
                await OnRendered.InvokeAsync(this);
            }
        }
    }
}
