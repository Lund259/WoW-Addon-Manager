using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.Addons.Controller.Concrete;
namespace IO.Addons.Controller
{
    public class AddonsControllerFactory
    {
        public IAddonController CreateAddonController()
        {
            return new AddonController();
        }
    }
}
