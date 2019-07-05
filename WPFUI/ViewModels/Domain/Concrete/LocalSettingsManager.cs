using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ViewModels.Domain.Concrete
{
    class LocalSettingsManager : ISettingsManager
    {
        public T GetSetting<T>(string key)
        {
            return (T)Properties.Settings.Default[key];
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        public void SetSetting<T>(string key, T value)
        {
            Properties.Settings.Default[key] = value;
        }
    }
}
