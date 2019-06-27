using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.Addons.Foundation;
using IO.Addons.Models;

namespace IO.Addons.Controller.Concrete
{
    class AddonController : IAddonController
    {
        private FoundationFactory foundationFactory;
        private IObjectBuilder objectBuilder;
        private IAddonIO addonIO;

        public AddonController()
        {
            foundationFactory = new FoundationFactory();
            objectBuilder = foundationFactory.CreateObjectBuilder();
            addonIO = foundationFactory.CreateAddonIO();
        }

        public List<IAddonInfo> GetAddons(string folderPath)
        {
            try
            {
                var metadata = addonIO.GetMetaData(folderPath);
                return objectBuilder.GetAddonInfo(metadata);
            }
            catch (Exception ex)
            {
                throw new Exception($"The provided folder: {folderPath} is invalid", ex);
            }
        }
    }
}
