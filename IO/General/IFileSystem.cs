using System.IO;
using System.Threading.Tasks;

namespace IO.General
{
    public interface IFileSystem
    {
        bool DirectoryIsValid(string path);
        bool DirectoryExists(string path);
        void DeleteDirectory(string directoryPath);

        T GetObjectFromFile<T>(string path);
        void SaveObjectToFile<T>(string path, T obj);

        Task ExtractZipToPath(string zipPath, string outputPath);

    }
}