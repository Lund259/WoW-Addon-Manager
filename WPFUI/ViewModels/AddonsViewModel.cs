using Caliburn.Micro;
using IO.Addons.Controller;
using IO.Addons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ViewModels
{
    public class AddonsViewModel : Screen
    {
        private AddonsControllerFactory addonControllerFactory = new AddonsControllerFactory();
        private IAddonController addonController;

        private IEnumerable<IAddonInfo> _addons;
        private string _searchTerm;

        public bool ShowSettingsPrompt
        {
            get
            {
                return string.IsNullOrWhiteSpace(Properties.Settings.Default.AddonFolder);

                
            }
        }

        public object SettingsPrompt { get; set; }

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

        public AddonsViewModel()
        {
            //testing
            Properties.Settings.Default.Reset();
            
            addonController = addonControllerFactory.CreateAddonController();

            SettingsPrompt = new Views.SettingsPrompt();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            if (!ShowSettingsPrompt && Addons == null)
                LoadAddons();
        }

        public void LoadAddons()
        {
            try
            {
                var addons = addonController.GetAddons(Properties.Settings.Default.AddonFolder);
                Addons = new BindableCollection<IAddonInfo>(addons);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show($"\"{Properties.Settings.Default.AddonFolder}\" is not a valid World of Warcraft root folder.");
            }
        }
    }
}
