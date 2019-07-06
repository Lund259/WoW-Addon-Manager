using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

        //Extract the provided zipfile to the outputpath and throw an exception if the zipfile is invalid.
        public Task ExtractZipToPath(string zipPath, string outputPath)
        {
            return Task.Run(() =>
            {
                try
                {
                    ZipFile.ExtractToDirectory(zipPath, outputPath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public T GetObjectFromFile<T>(string path)
        {
            //implement error logic. What if the path doesn't exist?

            using (StreamReader reader = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }

        public void MoveDirectory(string source, string target)
        {
            var sourcePath = source.TrimEnd('\\', ' ');
            var targetPath = target.TrimEnd('\\', ' ');
            var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories)
                                 .GroupBy(s => Path.GetDirectoryName(s));
            foreach (var folder in files)
            {
                var targetFolder = folder.Key.Replace(sourcePath, targetPath);
                Directory.CreateDirectory(targetFolder);
                foreach (var file in folder)
                {
                    var targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
                    if (File.Exists(targetFile)) File.Delete(targetFile);
                    File.Move(file, targetFile);
                }
            }
            Directory.Delete(source, true);
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
