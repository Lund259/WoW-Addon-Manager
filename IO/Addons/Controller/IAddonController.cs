using IO.Addons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Addons.Controller
{
    public interface IAddonController
    {
        List<IAddonInfo> GetAddons(string folderPath);

        string GetAddonsFolderPath(string rootFolder);

        void RemoveAddon(IAddonInfo addon);

        void AddAddon();
    }
}
