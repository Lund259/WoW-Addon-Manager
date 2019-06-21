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
using Screen = Caliburn.Micro.Screen;

namespace WPFUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private AddonsControllerFactory addonControllerFactory = new AddonsControllerFactory();
        private IAddonController addonController;

        private BindableCollection<IAddonInfo> _addons = new BindableCollection<IAddonInfo>();
        public BindableCollection<IAddonInfo> Addons
        {
            get { return _addons; }
            set
            {
                _addons = value;
                NotifyOfPropertyChange(() => Addons);
            }
        }

        public ShellViewModel()
        {
            addonController = addonControllerFactory.CreateAddonController();
        }

        public void LoadAddons()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderDialog.Description = "Select the root folder of your World of Warcraft installation.";


            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                var addons = addonController.GetAddons($@"{folderDialog.SelectedPath}\interface\addons");
                Addons = new BindableCollection<IAddonInfo>(addons);
            }
        }
    }
}
