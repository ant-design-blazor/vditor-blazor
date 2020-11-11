using System;
using System.Collections.Generic;
using System.Text;

namespace Vditor.Models
{
    public class Toolbar
    {
        public Toolbar()
        {
            Toolbars = new List<object>();
        }

        public List<object> Toolbars { get; set; }
    }

    public class CustomToolbar
    {
        public string Hotkey { get; set; }
        public string Name { get; set; }
        public string TipPosition { get; set; }
        public string Tip { get; set; }
        public string ClassName { get; set; }
        public string Icon { get; set; }
    }
}
