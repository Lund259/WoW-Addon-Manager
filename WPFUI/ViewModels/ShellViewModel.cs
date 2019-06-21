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

        private IEnumerable<IAddonInfo> _addons = new BindableCollection<IAddonInfo>();
        private string _searchTerm;

        public IEnumerable<IAddonInfo> Addons
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(SearchTerm))
                    return _addons.Where(addon => addon.Title.ToLower().Contains(SearchTerm.ToLower()));

                return _addons;
            }
            set
            {
                _addons = value;
                NotifyOfPropertyChange(() => Addons);
            }
        }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
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
                try
                {
                    var addons = addonController.GetAddons($@"{folderDialog.SelectedPath}\interface\addons");
                    Addons = new BindableCollection<IAddonInfo>(addons);
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show($"\"{folderDialog.SelectedPath}\" is not a valid World of Warcraft root folder.");
                }
            }
        }
    }
}
