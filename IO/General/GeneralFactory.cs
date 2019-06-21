using IO.General.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.General
{
    public class GeneralFactory
    {
        public IFileSystem CreateFileSystem()
        {
            return new FileSystem();
        }
    }
}
