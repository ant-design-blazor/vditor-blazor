using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Vditor
{
    public partial class Editor : ComponentBase
    {
        /// <summary>
        /// Markdown Content
        /// </summary>
        [Parameter]
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    _wattingUpdate = true;
                }
            }
        }

        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        /// <summary>
        /// Html Content
        /// </summary>
        [Parameter]
        public string Html { get; set; }

        [Parameter] public EventCallback<string> HtmlChanged { get; set; }

        [Inject] private EditorService EditorService { get; set; }

        private ElementReference _ref;

        private bool _wattingUpdate = false;
        private string _value;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await EditorService.CreateVditor(_ref, this);
            }
        }

        [JSInvokable]
        public void OnInput(string value)
        {
            _value = value;
            _wattingUpdate = false;

            if (ValueChanged.HasDelegate)
            {
                ValueChanged.InvokeAsync(value);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (Value != null)
            {
                await EditorService.SetValue(_ref, Value);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (_wattingUpdate)
            {
                await EditorService.SetValue(_ref, _value);
                _wattingUpdate = false;
            }
        }
    }
}