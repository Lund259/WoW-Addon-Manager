using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFUI.ViewModels.Domain.Concrete;

namespace WPFUI.ViewModels.Domain
{
    class DomainFactory
    {
        public ISettingsManager CreateSettingsManager()
        {
            return new LocalSettingsManager();
        }

        public ICommand CreateCommand(Action<object> execute)
        {
            return new Command(execute);
        }
    }
}
