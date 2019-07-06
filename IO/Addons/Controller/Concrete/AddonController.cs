using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.Addons.Foundation;
using IO.Addons.Models;
using IO.General;

namespace IO.Addons.Controller.Concrete
{
    class AddonController : IAddonController
    {
        private FoundationFactory foundationFactory;
        private IObjectBuilder objectBuilder;
        private IAddonIO addonIO;
        private IFileSystem fileSystem;

        public AddonController()
        {
            foundationFactory = new FoundationFactory();
            objectBuilder = foundationFactory.CreateObjectBuilder();
            addonIO = foundationFactory.CreateAddonIO();
            fileSystem = new GeneralFactory().CreateFileSystem();
        }

        public string GetAddonsFolderPath(string rootFolder)
        {
            string addonsPath = $"{rootFolder}\\Interface\\Addons";

            if (fileSystem.DirectoryIsValid(addonsPath) && fileSystem.DirectoryExists(addonsPath))
                return addonsPath;
            else
                return null;
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

        public void RemoveAddon(IAddonInfo addon)
        {
            fileSystem.DeleteDirectory(addon.DirectoryPath);
        }

        public void RemoveAddons(IEnumerable<IAddonInfo> addons)
        {
            foreach(IAddonInfo addon in addons)
            {
                fileSystem.DeleteDirectory(addon.DirectoryPath);
            }
        }

        public void InstallAddon(string addonPath, string folderPath)
        {
            //is it a Zip file or a folder?

            try
            {
                fileSystem.ExtractZipToPath(addonPath, folderPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"The provided file {addonPath} is not a valid zip file", ex);
            }

        }
    }
}
