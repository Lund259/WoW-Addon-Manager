using IO.Addons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IO.Addons.Foundation.Concrete
{
    class ObjectBuilder : IObjectBuilder
    {
        public List<IAddonInfo> GetAddonInfo(IEnumerable<Dictionary<string, string>> metaData)
        {
            
            List<IAddonInfo> result = new List<IAddonInfo>();

            foreach (var addon in metaData)
            {
                IAddonInfo newAddonInfo = null;

                //If the supplied data doesn't comply with the currently implementet standard for metadata, we skip the addon. 
                try
                {
                    newAddonInfo = ConvertToAddonInfo(addon);
                }
                catch (Exception)
                {
                    continue;
                }

                result.Add(newAddonInfo);
            }

            return result;
        }

        //Trims, verifies and converts all the data to the correct formats and returns an IAddonInfo.
        private IAddonInfo ConvertToAddonInfo(Dictionary<string, string> addon)
        {
            ModelFactory factory = new ModelFactory();

            //potential metadata properties. Some might be null.
            addon.TryGetValue("Title", out string title);
            addon.TryGetValue("Description", out string description);
            addon.TryGetValue("Author", out string author);
            addon.TryGetValue("Interface", out string tmpInterface);
            addon.TryGetValue("Version", out string tmpVersion);
            addon.TryGetValue("DefaultState", out string defaultState);
            addon.TryGetValue("RequiredDeps", out string tmpRequiredDependencies);
            addon.TryGetValue("SavedVariables", out string tmpSavedVariables);
            addon.TryGetValue("X-Website", out string tmpWebsite);
            addon.TryGetValue("X-Email", out string email);
            addon.TryGetValue("X-Category", out string category);

            //Metadata key for descriptions varies between Notes or Description. 
            if (description == null)
                addon.TryGetValue("Notes", out description);

            //make sure the version has the correct format. 
            //some versions includes codenames like: "1.1.1 (Kangoroo)". Other are formattet differently, for examble: V1.1.1
            //this regex match would return "1.1.1"
            Version version = null;
            if (tmpVersion != null)
            {
                string versionString = Regex.Match(tmpVersion, @"\d+(?:\.)\d+(?:\.)\d+").Value;

                int missingVersionNumbers = 3 - versionString.Split(',').Length;

                for (int i = missingVersionNumbers; i >= 0; i--)
                    versionString += ".0";
            }

            Version @interface = null;
            if (tmpInterface != null)
            {
                for (int i = 0; i < 5 - tmpInterface.Length; i++)
                    tmpInterface += '0';

                @interface = new Version(Convert.ToInt32(tmpInterface.Substring(0, 1)), Convert.ToInt32(tmpInterface.Substring(1, 2)), Convert.ToInt32(tmpInterface.Substring(3, 2)));
            }

            //if there are any required dependencies/saved variables, seperate them by comma and insert into arrays. 
            string[] requiredDependencies = null;
            string[] savedVariables = null;

            if (tmpRequiredDependencies != null)
                requiredDependencies = ReplaceWhitespace(tmpRequiredDependencies, "").Split(',');

            if (tmpSavedVariables != null)
                savedVariables = ReplaceWhitespace(tmpSavedVariables, "").Split(',');

            Uri website;
            Uri.TryCreate(tmpWebsite, UriKind.Absolute, out website);

            IAddonInfo newAddonInfo =
                factory.CreateAddonInfo(
                    title,
                    description,
                    author,
                    @interface,
                    version,
                    defaultState,
                    requiredDependencies,
                    savedVariables,
                    website,
                    email,
                    category
                    );
            return newAddonInfo;
        }

        private string ReplaceWhitespace(string input, string replacement)
        {
            return Regex.Replace(input, @"\s+", replacement);
        }
    }
}
