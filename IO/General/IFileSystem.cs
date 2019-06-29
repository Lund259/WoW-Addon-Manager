using System.Threading.Tasks;

namespace IO.General
{
    public interface IFileSystem
    {
        bool DirectoryIsValid(string path);
        bool DirectoryExists(string path);


        T GetObjectFromFile<T>(string path);

        void SaveObjectToFile<T>(string path, T obj);
    }
}