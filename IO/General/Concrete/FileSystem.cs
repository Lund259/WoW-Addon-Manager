using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.General.Concrete
{
    class FileSystem : IFileSystem
    {
        public void DeleteDirectory(string directoryPath)
        {
            if (DirectoryExists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
            else
            {
                throw new Exception($"{directoryPath} is not a valid directory.");
            }
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool DirectoryIsValid(string path)
        {
            Uri pathUri;
            Boolean isValidUri = Uri.TryCreate(path, UriKind.Absolute, out pathUri);
            return isValidUri && pathUri != null && pathUri.IsLoopback;
        }

        public T GetObjectFromFile<T>(string path)
        {
            //implement error logic. What if the path doesn't exist?

            using (StreamReader reader = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }

        public void SaveObjectToFile<T>(string path, T obj)
        {
            using (StreamWriter writer = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                
                serializer.Serialize(writer, obj);
            }
        }
    }
}
