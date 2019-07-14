using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ViewModels
{
    class ChangelogViewModel : Screen
    {
        public string CurrentVersion { get; set; }

        public ChangelogViewModel()
        {
            CurrentVersion = $"Your current version: {GetCurrentVersion()}";
        }

        private string GetCurrentVersion()
        {
            string currentVersionString = ApplicationDeployment.IsNetworkDeployed
               ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
               : System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return currentVersionString;
        }
    }
}
