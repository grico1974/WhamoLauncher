using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WhamoLauncher.Charts.ViewControllers;

namespace WhamoLauncher.Charts
{
    internal static class Program
    {
        public static readonly string ProductName;

        static Program()
        {
            ProductName = $"{Application.ProductName} { Application.ProductVersion}";
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var main = Controller.GetDefaultController();

            try
            {
                if (args.Length == 0)
                {
                    main.ShowErrorMessage(Strings.NoDataFileMessage, Strings.GenericErrorCaption);
                    return;
                }

                Logger.Default.SetLoggingPath(Path.Combine(Path.GetDirectoryName(args[0]), Strings.LogFileName));
                IEnumerable<ChartInfo> charts = null;

                using (var dialog = new ChartOptionsViewController(args[0]))
                {
                    charts = dialog.ShowViewDialog(main) as IEnumerable<ChartInfo>;
                }

                if (charts == null || !charts.Any())
                {
                    return;
                }

                var xlsTask = TaskManagerWithFeedbackDialog<WorkInProgressController>.WaitUntilCompleted(
                              () => XlsWorkbookBuilder.BuildExcelFile(Path.ChangeExtension(args[0], Strings.ExcelFileExtension), charts));

                if (!xlsTask.IsFaulted)
                {
                    Process.Start(xlsTask.Result.FullName);
                }
                else
                {
                    main.ShowErrorMessage(string.Format(Strings.CriticalErrorMessage, xlsTask.Exception.GetBaseException().Message),
                                          Strings.GenericErrorCaption);
                }
            }
            catch (Exception exc)
            {
                Logger.Default.Log(exc);
                main.ShowErrorMessage(string.Format(Strings.UnexpectedErrorMessage, Logger.Default.LoggingPath), Strings.GenericErrorCaption);
            }
        }
    }
}
