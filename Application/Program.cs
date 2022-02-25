using Common.LoggerManager;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RBAUtils
{
    static class Program
    {
        static private IConfiguration configuration;
        static private string parentLog;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            SetupEnvironment(args);

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Application());
        }

        static void SetupEnvironment(string[] args)
        {
            // attempt to synchronize writing to the main log - otherwise, create a separate log for sftp transfer status/errors
            //ParseArguments(args);

            // Get appsettings.json config - AddEnvironmentVariables() requires package: Microsoft.Extensions.Configuration.EnvironmentVariables
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // logger
            SetLogging();
        }

        static string[] GetLoggingLevels(int index)
        {
            return configuration.GetSection("LoggerManager:Logging").GetValue<string>("Levels").Split('|');
        }

        static void SetLogging()
        {
            try
            {
                string[] logLevels = GetLoggingLevels(0);

                if (logLevels.Length > 0)
                {
                    string fullName = Assembly.GetEntryAssembly().Location;
                    string logname = string.IsNullOrWhiteSpace(parentLog) ? Path.GetFileNameWithoutExtension(fullName) + ".log" : parentLog;
                    string path = Directory.GetCurrentDirectory();
                    string filepath = path + "\\logs\\" + logname;

                    int levels = 0;
                    foreach (string item in logLevels)
                    {
                        foreach (LOGLEVELS level in LogLevels.LogLevelsDictonary.Where(x => x.Value.Equals(item)).Select(x => x.Key))
                        {
                            levels += (int)level;
                        }
                    }

                    Logger.SetFileLoggerConfiguration(filepath, levels);

                    Logger.info($"{Assembly.GetEntryAssembly().GetName().Name} ({Assembly.GetEntryAssembly().GetName().Version}) - LOGGING INITIALIZED.");
                }
            }
            catch (Exception e)
            {
                Logger.error("main: SetupLogging() - exception={0}", e.Message);
            }
        }
    }
}
