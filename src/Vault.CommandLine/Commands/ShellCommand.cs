using System;
using Microsoft.Framework.Runtime.Common.CommandLine;

namespace Vault.CommandLine.Commands
{
    internal class ShellCommand
    {
        public static void Define(CommandLineApplication cmd, GlobalOptions global)
        {
            cmd.Name = "shell";
            cmd.Description = "starts an interactive shell for running Vault commands";
            cmd.OnExecute(() =>
            {
                // Remove the shell command from future invocations
                var app = cmd.Parent;
                app.Commands.Remove(cmd);
                while (true)
                {
                    Console.Write("vault> ");
                    string line = Console.ReadLine();
                    if (line.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        return 0;
                    }
                    var currentArgs = Native.CommandLine.SplitArgs(line);
                    app.Execute(currentArgs);
                }
            });
        }
    }
}
