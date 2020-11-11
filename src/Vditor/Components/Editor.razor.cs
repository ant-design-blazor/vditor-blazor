﻿using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Vditor.Models;
using System.Linq;

namespace Vditor
{
    public partial class Editor : ComponentBase, IDisposable
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

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Options { get; set; }

        [Parameter]
        public string Mode { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public string Height { get; set; }

        [Parameter]
        public string Width { get; set; }

        [Parameter]
        public string MinHeight { get; set; }

        [Parameter]
        public bool Outline { get; set; }

        [Parameter]
        public Toolbar Toolbar { get; set; }

        private ElementReference _ref;

        private bool _editorRendered = false;
        private bool _wattingUpdate = false;
        private string _value;

        private bool _afterFirstRender = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                _afterFirstRender = true;
                await CreateVditor();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Options ??= new Dictionary<string, object>();

            Options["Mode"] = Mode ?? "ir";
            Options["Placeholder"] = Placeholder ?? "";
            Options["Height"] = int.TryParse(Height, out var h) ? h : (object)Height;
            Options["Width"] = int.TryParse(Width, out var w) ? w : (object)Width;
            Options["MinHeight"] = int.TryParse(MinHeight, out var m) ? m : (object)MinHeight;
            Options["Options"] = Outline;

            if (Toolbar != null)
            {
                List<object> bars = new List<object>();
                foreach(var item in Toolbar.Buttons)
                {
                    if(item is string)
                    {
                        bars.Add(item);
                    }
                    else
                    {
                        var toolbar = item as CustomToolButton;
                        if (toolbar != null)
                        {
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic.Add("hotkey", toolbar.Hotkey);
                            dic.Add("name", toolbar.Name);
                            dic.Add("tipPosition", toolbar.TipPosition);
                            dic.Add("tip", toolbar.Tip);
                            dic.Add("className", toolbar.ClassName);
                            dic.Add("icon", toolbar.Icon);
                            dic.Add("click", "function() {\n clickCustomToolbar('" + toolbar.Name + "') \n}");
                            bars.Add(dic);
                        }
                    }
                }
                Options["Toolbar"] = bars;
            }

        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (_wattingUpdate && _editorRendered)
            {
                _wattingUpdate = false;
                await SetValue(_value, true);
            }
        }

        public async ValueTask<string> GetValueAsync()
        {
            if (_editorRendered)
            {
                return await GetValue();
            }

            return string.Empty;
        }

        public void Dispose()
        {
            Destroy();
        }
    }
}