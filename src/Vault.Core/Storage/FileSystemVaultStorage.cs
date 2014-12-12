using System.IO;
using System.Threading.Tasks;

namespace Vault.Storage
{
    public class FileSystemVaultStorage : IVaultStorage
    {
        private DirectoryInfo _directory;

        public FileSystemVaultStorage(string directory)
        {
            _directory = new DirectoryInfo(directory);
        }

        public string Location
        {
            get
            {
                return _directory.FullName;
            }
        }

        public static FileSystemVaultStorage FromBaseDirectory(string baseDirectory)
        {
            return new FileSystemVaultStorage(Path.Combine(baseDirectory, ".vault"));
        }

        public Task<bool> Exists()
        {
            return Task.FromResult(_directory.Exists);
        }

        public Task Initialize()
        {
            if (_directory.Exists)
            {
                _directory.Delete(recursive: true);
            }
            _directory.Create();
            return Task.FromResult(0);
        }

        public Task Open()
        {
            // Nothing to do to open
            return Task.FromResult(0);
        }
    }
}
