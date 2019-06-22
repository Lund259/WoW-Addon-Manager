using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Models;
using IO.Addons.Controller;
using IO.Addons.Models;
using IO.General;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WPFUI.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>
    {

        public void LoadAddonsView()
        {
            ActivateItem(new AddonsViewModel());
        }

        public void LoadSettingsView()
        {
            ActivateItem(new SettingsViewModel());
        }
    }
}
