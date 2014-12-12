using Microsoft.Framework.Runtime.Common.CommandLine;

namespace Vault.CommandLine.Commands
{
    internal class InitCommand
    {
        public static void Define(CommandLineApplication cmd, GlobalOptions global)
        {
            cmd.Name = "init";
            cmd.Description = "initializes a new vault in the current (or specified) directory";
            cmd.OnExecute(async () =>
            {
                // Get the vault directory
                AnsiConsole.Output.WriteLine("Creating vault in " + global.BaseDir);

                // Create a vault!
                using (var session = await VaultSession.InitializeNewVault(global.BaseDir))
                {
                    AnsiConsole.Output.WriteLine("Vault created.");
                }
                return 0;
            });
        }
    }
}
