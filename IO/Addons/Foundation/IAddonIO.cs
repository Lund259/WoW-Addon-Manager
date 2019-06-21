using System.Collections.Generic;

namespace IO.Addons.Foundation
{
    interface IAddonIO
    {
        IEnumerable<Dictionary<string, string>> GetMetaData(string folderPath);
    }
}