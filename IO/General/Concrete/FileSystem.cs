using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.General.Concrete
{
    class FileSystem : IFileSystem
    {
        public bool DirectoryIsValid(string path)
        {
            Uri pathUri;
            Boolean isValidUri = Uri.TryCreate(path, UriKind.Absolute, out pathUri);
            return isValidUri && pathUri != null && pathUri.IsLoopback;
        }
    }
}
