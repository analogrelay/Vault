using System;
using Microsoft.Framework.Runtime.Common.CommandLine;

namespace Vault.CommandLine
{
    internal class GlobalOptions
    {
        private CommandOption _baseDir;

        public string BaseDir { get { return _baseDir.HasValue() ? _baseDir.Value() : Environment.CurrentDirectory; } }

        public GlobalOptions(CommandOption baseDir)
        {
            _baseDir = baseDir;
        }
    }
}