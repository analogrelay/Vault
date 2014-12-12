using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vault.Storage
{
    public interface IVaultStorage
    {
        string Location { get; }

        Task<bool> Exists();
        Task Initialize();
        Task Open();
    }
}
