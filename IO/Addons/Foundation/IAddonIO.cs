using System.Collections.Generic;
using System.Threading.Tasks;

namespace IO.Addons.Foundation
{
    interface IAddonIO
    {
        IEnumerable<Dictionary<string, string>> GetMetaData(string folderPath);

        Task<bool> InstallAddon(string basePath, string addonPath);
    }
}