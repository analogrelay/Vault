using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Framework.Runtime.Common.CommandLine;
using Vault.CommandLine.Commands;

namespace Vault.CommandLine
{
    public class Program
    {
        static int Main(string[] args)
        {
            TryLaunchDebugger(ref args);

            var app = new CommandLineApplication(throwOnUnexpectedArg: true);
            app.Name = typeof(Program).Assembly.GetName().Name;
            app.Version = typeof(Program).Assembly.GetName().Version.ToString();
            app.HelpOption("-h|--help");
            app.VersionOption("-v|--version", string.Format("{0} {1}", app.Name, app.Version));

            var globalOptions = new GlobalOptions(
                baseDir: app.Option("-dir|--base-dir <directory>", "the directory to use as the base directory (defaults to the current directory)", CommandOptionType.SingleValue));

            // Initialize commands
            foreach (var type in FindCommands())
            {
                var name = type.Name.Substring(0, type.Name.Length - "Command".Length).ToLowerInvariant();
                app.Command(name, cmd => DefineCommand(cmd, globalOptions, type), addHelpCommand: false, throwOnUnexpectedArg: true);
            }

            // Default to help if no command specified
            app.OnExecute(() => { app.ShowHelp(commandName: null); return 0; });

            try
            {
                return app.Execute(args);
            }
            catch (AggregateException aex)
            {
                foreach (var innerEx in aex.InnerExceptions)
                {
                    WriteException(innerEx);
                }
                return -1;
            }
            catch (Exception ex)
            {
                WriteException(ex);
                return -1;
            }
        }

        private static void WriteException(Exception ex)
        {
            AnsiConsole.Output.WriteLine(ex.Message.Red().Bold());
#if DEBUG
            AnsiConsole.Output.WriteLine(ex.StackTrace.Gray());
#endif
        }

        private static object DefineCommand(CommandLineApplication cmd, GlobalOptions globalOptions, Type type)
        {
            return type.InvokeMember("Define", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, Type.DefaultBinder, target: null, args: new object[] { cmd, globalOptions });
        }

        private static IEnumerable<Type> FindCommands()
        {
            return typeof(Program)
                .Assembly
                .GetTypes()
                .Where(t =>
                    t.Namespace.Equals(typeof(Program).Namespace + ".Commands") &&
                    t.Name.EndsWith("Command", StringComparison.OrdinalIgnoreCase));
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private static void TryLaunchDebugger(ref string[] args)
        {
            if (args.Length > 0 && (string.Equals("dbg", args[0], StringComparison.OrdinalIgnoreCase) || string.Equals("debug", args[0], StringComparison.OrdinalIgnoreCase)))
            {
                args = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Skip(args, 1));
                System.Diagnostics.Debugger.Launch();
            }
        }
    }
}
