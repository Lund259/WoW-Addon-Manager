using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.Addons.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace IO.Addons.Foundation.Concrete
{
    class AddonIO : IAddonIO
    {

        //iterates through all the folders (to a depth of 2), finds the folders containing an addon, renames the folder correctly and moves it to the basePath
        //returns true if at least 1 addon were found in the addonPath
        //This method is definetely very poorly implemented. Should probably implement a recursive method with an optional folder depth. Just haven't had the time yet. it works for now.
        public Task<bool> InstallAddon(string basePath, string addonPath)
        {
            return Task.Run(() => 
            {
                bool result = false;
                string[] directories = Directory.GetDirectories(addonPath);

                foreach (string dir in directories)
                {
                    string[] tocFiles = Directory.GetFiles(dir, "*.toc", SearchOption.TopDirectoryOnly);

                    if (tocFiles.Length == 0)
                    {
                        foreach (string dir1 in Directory.GetDirectories(dir))
                        {
                            string[] tocFiles1 = Directory.GetFiles(dir1, "*.toc", SearchOption.TopDirectoryOnly);

                            if (tocFiles1.Length > 0)
                            {
                                //if a tocfile exists(that means it is an addon rootfolder): rename the folder correctly and move it folder to basePath (the folder containing all addons). 
                                CleanAndMoveAddon(basePath, dir1, tocFiles1[0]);
                                result = true;
                            }
                        }
                    }
                    else
                    {
                        //if a tocfile exists(that means it is an addon rootfolder): rename the folder correctly and move it folder to basePath (the folder containing all addons). 
                        CleanAndMoveAddon(basePath, dir, tocFiles[0]);
                        result = true;
                    }
                }

                return result;

            });
        }

        //rename the folder correctly and move it to the output path
        //Note: A folder must be named exactly the same as the toc file for the addon to load correctly in the game. 
        private static void CleanAndMoveAddon(string outputPath, string addonPath, string tocFile)
        {
            
            int tocNameIndex = tocFile.LastIndexOf('\\') + 1;

            //convert folder\\folder1\\folder2\\folder2.toc to: folder2
            string name = tocFile.Substring(tocNameIndex, tocFile.Length - 4 - tocNameIndex);

            Directory.Move(addonPath, $"{outputPath}\\{name}");
        }

        public IEnumerable<Dictionary<string, string>> GetMetaData(string folderPath)
        {
            List<Dictionary<string, string>> addons = new List<Dictionary<string, string>>();


            //Loop through each folder and extract the metadata from the .toc file(s) if any exists. 
            //Only loop through TopDirectory to increase performance.
            foreach (string addonFolderPath in GetAddonFolderPaths(folderPath))
            {
                IEnumerable<string> tocFiles = Directory.GetFiles(addonFolderPath, "*.toc", SearchOption.TopDirectoryOnly);
                if (tocFiles == null || !tocFiles.Any())
                    continue;

                foreach (string tocFile in tocFiles)
                {
                    var tocLines = File.ReadLines(tocFile);
                    var metaData = GetMetadata(tocLines);

                    //add directory
                    metaData.Add("DirectoryPath", addonFolderPath);

                    //Only include the addon if it has a title. (Some addons have helper addons without titles which we don't want to include)
                    if (metaData.ContainsKey("Title"))
                        addons.Add(metaData);
                }

            }

            return addons;
        }



        //Get all valid addon folders in the supplied directory.
        private IEnumerable<string> GetAddonFolderPaths(string addonRootFolder)
        {
            return Directory.GetDirectories(addonRootFolder).Where(folderName => !folderName.Contains("Blizzard_"));
        }

        //Go through the provided *.toc file line by line and extract the addon metadata to a dictionary. 
        private Dictionary<string, string> GetMetadata(IEnumerable<string> tocLines)
        {
            Dictionary<string, string> metaData = new Dictionary<string, string>();

            foreach (string line in tocLines)
            {
                //Exambles of correct line format: "## Interface: 30300"     "## X-Website: http://www.wowace.com/projects/bartender4/"
                const string regExpression = @"^## (?:.+)(?:\:)(?:.+)$";

                if (!Regex.IsMatch(line, regExpression))
                    continue;

                //begin right after the "##" metadata declaration. Excluding the whitespace (therefore 3, not 2).
                int keyStart = 3;
                int keyValueSeperatorIndex = line.IndexOf(':');
                // ':' marks the end of the metadata key. 
                string key = line.Substring(keyStart, keyValueSeperatorIndex - keyStart);

                // start at the seperator and skip the whitespace if it exists. 
                int valueStart = line[keyValueSeperatorIndex+1] == ' ' ? keyValueSeperatorIndex + 2 : keyValueSeperatorIndex + 1;
                string value = line.Substring(valueStart);


                //remove colorcodes and styling from the value 
                //examble: "|cffffd200Deadly Boss Mods|r |cff69ccf0Core|r" becomes: "Deadly Boss Mods Core"
                value = Regex.Replace(value, @"\|[a-fA-F0-9]{9}|\|r|\|n", "");

                //Remove version numbers from the title. (as they are stored seperatly).
                if (key == "Title")
                    value = Regex.Replace(value, @"[0-9]+(\.[0-9]+)*$", "");

                //if the metadata key allready exists, append the extra value to the existing one (seperated by a comma).
                if (metaData.ContainsKey(key))
                    metaData[key] += $", {value}";
                else
                    metaData.Add(key, value);
            }


            return metaData;
        }
    }
}
