using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WhamoLauncher.Charts.Views;

namespace WhamoLauncher.Charts.ViewControllers
{
    internal class ChartOptionsViewController : Controller<ChartOptionsView>
    {
        private enum ChartOption
        {
            NoCharts = 0,
            DefaultCharts,
            CustomCharts,
        }

        private string inputFilePath;
        private ChartOption selectedOption;
        
        public ChartOptionsViewController(string csvFilePath)
        {
            Debug.Assert(csvFilePath != null);
            inputFilePath = csvFilePath;
        }

        public override void ProcessCommand(Control control, Command command)
        {
            if (command == Command.Default)
            {
                selectedOption = ChartOption.DefaultCharts;
            }
            else if (command == Command.Custom)
            {
                selectedOption = ChartOption.CustomCharts;
            }
            else
            {
                selectedOption = ChartOption.NoCharts;
            }

            View.Close();
        }

        private static IEnumerable<ChartInfo> buildDefaultCharts(OutputData data) =>
            OutputData.GetAllSeries(data)
                      .Select(s => new ChartInfo(data.Title, s.Name.Split(new string[] { Strings.SeriesHeaderSeparator },
                                                                                         StringSplitOptions.RemoveEmptyEntries)[1],
                                                 new SeriesInfo[] { s }));

        public override object ShowViewDialog(IViewController owner)
        {
            IEnumerable<ChartInfo> charts = null;
            View.ShowDialog(owner.View);

            if (selectedOption != ChartOption.NoCharts)
            {
                var data = OutputData.GetData(inputFilePath);

                if (selectedOption == ChartOption.DefaultCharts)
                {
                    charts = buildDefaultCharts(data);
                }
                else if (selectedOption == ChartOption.CustomCharts)
                {
                    using (var custom = new CustomChartsController(data, Path.ChangeExtension(inputFilePath, Strings.GraphTemplateFileExtension)))
                    {
                        charts = custom.ShowViewDialog(this) as IEnumerable<ChartInfo>;
                    }

                    if (charts != null && charts.Any())
                    {
                        GraphTemplateStream.Write(Path.ChangeExtension(inputFilePath, Strings.GraphTemplateFileExtension), charts);
                    }
                }
            }

            return charts;
        }
    }
}
