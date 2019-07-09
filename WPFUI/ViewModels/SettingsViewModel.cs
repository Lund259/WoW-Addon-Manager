using Caliburn.Micro;
using IO.Addons.Controller;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPFUI.ViewModels.Domain;
using Screen = Caliburn.Micro.Screen;

namespace WPFUI.ViewModels
{
    public class SettingsViewModel : Screen
    {
        private DomainFactory domainFactory = new DomainFactory();
        private ISettingsManager settingsManager;

        private string _folderPath;

        public string FolderPath
        {
            get { return _folderPath; }
            set
            {
                _folderPath = value;
                NotifyOfPropertyChange(() => FolderPath);
            }
        }

        private bool _uninstallConfirmation;
        public bool UninstallConfirmation
        {
            get { return _uninstallConfirmation; }
            set
            {
                _uninstallConfirmation = value;
                NotifyOfPropertyChange(() => UninstallConfirmation);
            }
        }
        public ISnackbarMessageQueue NotificationQueue { get; set; }

        IAddonController addonController;


        public SettingsViewModel()
        {
            addonController = new AddonsControllerFactory().CreateAddonController();
            settingsManager = domainFactory.CreateSettingsManager();

            NotificationQueue = new SnackbarMessageQueue();
        }

        public void LoadCurrentSettings()
        {
            FolderPath = settingsManager.AddonsFolder;
            UninstallConfirmation = settingsManager.UninstallConfirmation;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            LoadCurrentSettings();
        }

        public void ShowFolderDialog()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderDialog.Description = "Select the root folder of your World of Warcraft installation.";


            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string tempPath = addonController.GetAddonsFolderPath(folderDialog.SelectedPath);
                if (!string.IsNullOrWhiteSpace(tempPath))
                {
                    FolderPath = tempPath;
                }
                else
                {
                    DisplayNotification("Please select a valid WoW Root folder.");
                }
            }
        }

        //Return false if the settings are invalid
        public bool CanSaveSettings(string folderPath, bool uninstallConfirmation)
        {
            if (!string.IsNullOrWhiteSpace(folderPath))
                return true;

            return false;
        }

        public void SaveSettings(string folderPath, bool uninstallConfirmation)
        {
            settingsManager.AddonsFolder = folderPath;
            settingsManager.UninstallConfirmation = UninstallConfirmation;

            DisplayNotification("Settings successfully saved!");
        }

        public void DisplayNotification(string message)
        {
            Task.Factory.StartNew(() => NotificationQueue.Enqueue(message, true));
        }

    }
}
