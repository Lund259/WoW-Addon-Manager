using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{
    public class Page
    {
        public string Name { get; set; }
        public Screen Screen { get; set; }

        public string Icon { get; set; }

        public Page(string name, Screen screen, string icon)
        {
            Name = name;
            Screen = screen;
            Icon = icon;
        }


    }
}
