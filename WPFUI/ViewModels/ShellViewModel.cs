using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.Addons.Controller;
using IO.Addons.Models;
using IO.General;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Screen = Caliburn.Micro.Screen;
using WPFUI.Models;

namespace WPFUI.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>
    {
        public Page[] Pages { get; set; }


        private Page _currentPage;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                LoadView();
            }
        }



        public ShellViewModel()
        {
            Pages = new Page[] { new Page("Addons", new AddonsViewModel()), new Page("Settings", new SettingsViewModel())};
            CurrentPage = Pages[0];
        }

        public void LoadView()
        {
            ActivateItem(CurrentPage.Screen);
        }

    }
}
