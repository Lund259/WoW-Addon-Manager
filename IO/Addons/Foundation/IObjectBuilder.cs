using System.Collections.Generic;
using IO.Addons.Models;

namespace IO.Addons.Foundation
{
    interface IObjectBuilder
    {
        List<IAddonInfo> GetAddonInfo(IEnumerable<Dictionary<string, string>> metaData);
    }
}