using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.Addons.Models.Concrete;

namespace IO.Addons.Models
{
    class ModelFactory
    {
        public IAddonInfo CreateAddonInfo(string title, string description, string author, Version @interface, Version version, string defaultState, string[] requiredDependencies, string[] savedVariables, Uri website, string email, string category, string directoryPath)
        {
            return new AddonInfo()
            {
                Title = title,
                Description = description,
                Author = author,
                Interface = @interface,
                Version = version,
                DefaultState = defaultState,
                RequiredDependencies = requiredDependencies,
                SavedVariables = savedVariables,
                Website = website,
                Email = email,
                Category = category,
                DirectoryPath = directoryPath
            };
        }
    }
}
