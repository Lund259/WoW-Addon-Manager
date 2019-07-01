using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Addons.Models.Concrete
{
    class AddonInfo : IAddonInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Version Interface { get; set; }
        public Version Version { get; set; }
        public string DefaultState { get; set; }
        public string[] RequiredDependencies { get; set; }
        public string[] SavedVariables { get; set; }
        public Uri Website { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string DirectoryPath { get; set; }
    }
}
