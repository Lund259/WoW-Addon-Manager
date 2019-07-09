using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ViewModels.Domain.Concrete
{
    class LocalSettingsManager : ISettingsManager
    {
        public string AddonsFolder
        {
            get
            {
                return Properties.Settings.Default.AddonsFolder;
            }
            set
            {
                Properties.Settings.Default.AddonsFolder = value;
                Properties.Settings.Default.Save();
            }

        }

        public bool UninstallConfirmation
        {
            get
            {
                return Properties.Settings.Default.UninstallConfirmation;

            }
            set
            {
                Properties.Settings.Default.UninstallConfirmation = value;
                Properties.Settings.Default.Save();
            }
        }


    }
}
