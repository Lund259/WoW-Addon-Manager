using Caliburn.Micro;
using IO.Addons.Controller;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Screen = Caliburn.Micro.Screen;

namespace WPFUI.ViewModels
{
    public class SettingsViewModel : Screen
    {
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

            NotificationQueue = new SnackbarMessageQueue();
        }

        public void ShowFolderDialog()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderDialog.Description = "Select the root folder of your World of Warcraft installation.";


            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string tempPath = addonController.GetAddonFolderPath(folderDialog.SelectedPath);
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

            if (string.IsNullOrWhiteSpace(folderPath) || folderPath == Properties.Settings.Default.AddonFolder)
                result = false;

            return result;
        }

        public void SaveSettings(string folderPath)
        {
            Properties.Settings.Default.AddonFolder = folderPath;
            Properties.Settings.Default.Save();

            DisplayNotification("Settings successfully saved!");
        }

        public void DisplayNotification(string message)
        {

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => NotificationQueue.Enqueue(message));
        }

    }
}
