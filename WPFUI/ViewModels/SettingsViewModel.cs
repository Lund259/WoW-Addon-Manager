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

        //Return false if the settings are invalid or the settings hasn't changed. 
        public bool CanSaveSettings(string folderPath)
        {
            bool result = true;

            if (string.IsNullOrWhiteSpace(folderPath) || folderPath == settingsManager.AddonsFolder)
                result = false;

            return result;
        }

        public void SaveSettings(string folderPath)
        {
            settingsManager.AddonsFolder = folderPath;

            DisplayNotification("Settings successfully saved!");
        }

        public void DisplayNotification(string message)
        {

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => NotificationQueue.Enqueue(message));
        }

    }
}
