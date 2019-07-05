using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ViewModels.Domain
{
    interface ISettingsManager
    {
        T GetSetting<T>(string key);
        void SetSetting<T>(string key, T value);
        void SaveSettings();
    }
}
