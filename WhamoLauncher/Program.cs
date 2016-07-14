using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using WhamoLauncher.Charts;

namespace WhamoLauncher
{
    internal static class Program
    {
        public const int SuccesfulExitCode = 0;
        public const int NoArgumentExitCode = 1;
        public const int ExecutionErrorExitCode = 2;

        [STAThread]
        static int Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            Console.Title = string.Format(CultureInfo.CurrentCulture, "{0} {1}: {2}", assembly.GetCustomAttributes(true)
                                                                                              .OfType<AssemblyCopyrightAttribute>()
                                                                                              .First().Copyright,
                                          assemblyName.Name, assemblyName.Version.ToString());
 
            if (args.Length == 0)
            {
                Console.WriteLine(Strings.NoInputFileMessage);
                Console.WriteLine(Strings.PressKeyToExitMessage);
                Console.ReadKey();
                return NoArgumentExitCode;
            }

            try
            {
                Launcher.Launch(args[0]/*, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Strings.WhamoExecutableFileName)*/);
                Console.WriteLine();
                var csvFile = Path.ChangeExtension(args[0], Strings.CsvFileExtension);
                var chartsProcStartInfo = new ProcessStartInfo("WhamoLauncher.Charts.exe", string.Format("\"{0}\"", csvFile));
                chartsProcStartInfo.UseShellExecute = false;
                var chartsProc = Process.Start(chartsProcStartInfo);
                return SuccesfulExitCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(Strings.PressKeyToExitMessage);
                Console.ReadKey();
                return ExecutionErrorExitCode;
            }
        }
    }
}
