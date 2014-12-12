namespace Microsoft.Framework.Runtime.Common.CommandLine
{
    public static class Ansi
    {
        public static readonly string Bold = "\x1b[1m";
        public static readonly string NormalWeight = "\x1b[22m";
        public static readonly string Black = "\x1b[30m";
        public static readonly string Red = "\x1b[31m";
        public static readonly string Green = "\x1b[32m";
        public static readonly string Yellow = "\x1b[33m";
        public static readonly string Blue = "\x1b[34m";
        public static readonly string Magenta = "\x1b[35m";
        public static readonly string Cyan = "\x1b[36m";
        public static readonly string Gray = "\x1b[37m";
        public static readonly string ResetColor = "\x1b[39m";
    }

    public static class StringExtensions
    {
        public static string Bold(this string str) { return Ansi.Bold + str + Ansi.NormalWeight; }
        public static string Black(this string str) { return Ansi.Black + str + Ansi.ResetColor; }
        public static string Red(this string str) { return Ansi.Red + str + Ansi.ResetColor; }
        public static string Green(this string str) { return Ansi.Green + str + Ansi.ResetColor; }
        public static string Yellow(this string str) { return Ansi.Yellow + str + Ansi.ResetColor; }
        public static string Blue(this string str) { return Ansi.Blue + str + Ansi.ResetColor; }
        public static string Magenta(this string str) { return Ansi.Magenta + str + Ansi.ResetColor; }
        public static string Cyan(this string str) { return Ansi.Cyan + str + Ansi.ResetColor; }
        public static string Gray(this string str) { return Ansi.Gray + str + Ansi.ResetColor; }
    }
}
