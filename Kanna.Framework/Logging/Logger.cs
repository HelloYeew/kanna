using System.Globalization;
using System.Reflection;

namespace Kanna.Framework.Logging
{
    public class Logger
    {
        public static string User = Environment.UserName;

        private static void log(string message, LoggingTarget target = LoggingTarget.Runtime, LogLevel level = LogLevel.Verbose)
        {
#if !DEBUG
            if (level <= LogLevel.Debug) return;
#endif
            string[] lines = message.TrimEnd().Replace(@"\r\n", @"\n").Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string s = lines[i];
                lines[i] = $@"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)} [{level.ToString().ToLowerInvariant()}]: {s.Trim()}";
            }

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Log an arbitrary string to a target.
        /// </summary>
        /// <param name="message">The message to log. Can include newline (\n) characters to split into multiple lines.</param>
        /// <param name="level">The log level to use.</param>
        /// <param name="target">The target to log to.</param>
        public static void Log(string message, LoggingTarget target = LoggingTarget.Runtime, LogLevel level = LogLevel.Verbose)
        {
            log(message, target, level);
        }

        /// <summary>
        /// Log some essential information about the current runtime.
        /// </summary>
        public static void AddHeader()
        {
            log($@"----------------------------------------------");
            log($@"Log for {User}");
            log($@"Running {Assembly.GetEntryAssembly()?.GetName().Name} on .NET {Environment.Version} using Kanna Framework version {Assembly.GetExecutingAssembly().GetName().Version}");
            log($@"Environment : {Environment.OSVersion.Platform} {Environment.OSVersion.Version}, {Environment.ProcessorCount} cores ({(Environment.Is64BitProcess ? "64" : "32")}bit process)");
            log($@"----------------------------------------------");
        }
    }
}
