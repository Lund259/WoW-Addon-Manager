using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WPFUI.Models;

namespace WPFUI.ViewModels
{
    class ChangelogViewModel : Screen
    {
        public string CurrentVersion { get; set; }
        public IEnumerable<VersionLog> ChangeLog { get; set; }


        public ChangelogViewModel()
        {
            CurrentVersion = $"Your current version: {GetCurrentVersion()}";
            ChangeLog = GetChangeLog();
        }

        private string GetCurrentVersion()
        {
            string currentVersionString = ApplicationDeployment.IsNetworkDeployed
               ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
               : System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return currentVersionString;
        }

        private string GetChangeLogText()
        {
            string result = "";

            using(WebClient client = new WebClient())
                result = client.DownloadString("https://raw.githubusercontent.com/Lund259/WoW-Addon-Manager/Development/CHANGELOG.md");

            return result;
        }

        private List<VersionLog> GetChangeLog()
        {
            List<VersionLog> result = new List<VersionLog>();

            //Github md format
            string changeLogText = GetChangeLogText();

            string[] changeLogLines = changeLogText.Split(new string[] { @"#" }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach(string version in changeLogLines)
            {
                //Mathces V0.2, V0.2.1
                Match titleMatch = Regex.Match(version, @"(V)(\*|\d+(\.\d+){0,2}(\.\*)?)");

                string title = titleMatch.Value;
                string description = version.Substring(titleMatch.Index + titleMatch.Length);

                result.Add(new VersionLog(title, description));
            }

            return result;
        }
    }
}
