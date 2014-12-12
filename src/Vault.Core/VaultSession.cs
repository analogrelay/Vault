using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vault.Storage;

namespace Vault
{
    public class VaultSession : IDisposable
    {
        private IVaultStorage _storage;

        private VaultSession(IVaultStorage storage)
        {
            _storage = storage;
        }

        public static Task<VaultSession> InitializeNewVault(string directory)
        {
            return InitializeNewVault(FileSystemVaultStorage.FromBaseDirectory(directory));
        }

        public static async Task<VaultSession> InitializeNewVault(IVaultStorage storage)
        {
            if (await storage.Exists())
            {
                throw new InvalidOperationException(string.Format(Strings.VaultSession_VaultAlreadyExists, storage.Location));
            }
            await storage.Initialize();
            return new VaultSession(storage);
        }

        public static Task<VaultSession> OpenExistingVault(string directory)
        {
            return OpenExistingVault(FileSystemVaultStorage.FromBaseDirectory(directory));
        }

        public static async Task<VaultSession> OpenExistingVault(IVaultStorage storage)
        {
            if (!await storage.Exists())
            {
                throw new InvalidOperationException(string.Format(Strings.VaultSession_VaultDoesNotExist, storage.Location));
            }
            await storage.Open();
            return new VaultSession(storage);
        }

        public void Dispose()
        {
            var disposableStorage = _storage as IDisposable;
            if (disposableStorage != null)
            {
                disposableStorage.Dispose();
            }
        }
    }
}
