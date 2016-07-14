using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WhamoLauncher.Charts.Views;

namespace WhamoLauncher.Charts.ViewControllers
{
    internal class CustomChartsController : Controller<CustomChartsView>
    {
        private readonly OutputData data;
        private readonly string templateFilePath;

        public CustomChartsController(OutputData data, string templateFilePath)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (templateFilePath == null)
            {
                throw new ArgumentNullException(nameof(templateFilePath));
            }

            this.data = data;
            this.templateFilePath = templateFilePath;
        }

        public override void ProcessCommand(Control control, Command command)
        {
            Debug.Assert(View.SelectedTab == null ||
                         (View.SelectedTab.Controls.Count > 0 && View.SelectedTab.Controls[0] is ChartSetup));

            if (command == Command.AddPrimary)
            {
                var chartSetup = View.SelectedTab.Controls[0] as ChartSetup;
                var selectedItems = chartSetup.SelectedSeries.Cast<SeriesInfo>().ToList();
                var allDataSource = chartSetup.DataSource as IList;
                var primaryDataSource = chartSetup.PrimarySeries;

                foreach (var s in selectedItems)
                {
                    allDataSource.Remove(s);
                    primaryDataSource.Add(s);
                    s.ShowInSecondaryVerticalAxis = false;
                }
            }
            else if (command == Command.AddSecondary)
            {
                var chartSetup = View.SelectedTab.Controls[0] as ChartSetup;
                var selectedItems = chartSetup.SelectedSeries.Cast<SeriesInfo>().ToList();
                var allDataSource = chartSetup.DataSource as IList;
                var secondaryDataSource = chartSetup.SecondarySeries;

                foreach (var s in selectedItems)
                {
                    allDataSource.Remove(s);
                    secondaryDataSource.Add(s);
                    s.ShowInSecondaryVerticalAxis = true;
                }
            }
            else if (command == Command.RemovePrimary)
            {
                var chartSetup = View.SelectedTab.Controls[0] as ChartSetup;
                var selectedItems = chartSetup.SelectedPrimarySeries.ToList();
                var allDataSource = chartSetup.DataSource as IList;
                var primaryDataSource = chartSetup.PrimarySeries;

                foreach (var s in selectedItems)
                {
                    primaryDataSource.Remove(s);
                    allDataSource.Add(s);
                }
            }
            else if (command == Command.RemoveSecondary)
            {
                var chartSetup = View.SelectedTab.Controls[0] as ChartSetup;
                var selectedItems = chartSetup.SelectedSecondarySeries.ToList();
                var allDataSource = chartSetup.DataSource as IList;
                var secondaryDataSource = chartSetup.SecondarySeries;

                foreach (var s in selectedItems)
                {
                    secondaryDataSource.Remove(s);
                    allDataSource.Add(s);
                }
            }
            else if (command == Command.Load)
            {
                var series = OutputData.GetAllSeries(data).ToList();

                try
                {
                    control.Enabled = false;
                    View.ClearTabs();
                    var graphs = GraphTemplateStream.Read(templateFilePath, data);

                    foreach (var graph in graphs)
                    {
                        ProcessCommand(null, Command.AddTab);
                        loadGraph(View.SelectedTab.Controls[0] as ChartSetup, graph);
                    }

                    control.Enabled = true;
                }
                catch (Exception exc)
                {
                    if (exc is FormatException)
                    {
                        View.ShowErrorMessage(Strings.FormatExceptionMessage, Strings.FormatExceptionCaption);
                    }
                    else if (exc is InvalidOperationException)
                    {
                        View.ShowErrorMessage(Strings.InvalidOperationExceptionMessage, Strings.InvalidOperationExceptionCaption);
                    }
                    else if (exc is IOException)
                    {
                        View.ShowErrorMessage(Strings.IOExceptionMessage, Strings.IOExceptionCaption);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else if (command == Command.AddTab)
            {
                var newTab = View.AddNewTab();
                (View.SelectedTab.Controls[0] as ChartSetup).DataSource = new BindingList<SeriesInfo>(OutputData.GetAllSeries(data).ToList());
            }
            else if (command == Command.RemoveTab)
            {
                Debug.Assert(View.SelectedTab != null);
                View.RemoveTab(View.SelectedTab);
            }
            else if (command == Command.Accept)
            {
                View.DialogResult = DialogResult.OK;
                View.Close();
            }
            else if (command == Command.Cancel)
            {
                View.DialogResult = DialogResult.Cancel;
                View.Close();
            }
            else
            {
                Debug.Assert(false, "command");
            }
        }

        public override object ShowViewDialog(IViewController owner)
        {
            var dialogResult = View.ShowDialog(owner.View);

            if (dialogResult == DialogResult.OK)
            {
                return generateCharts();
            }

            return null;
        }

        private static void loadGraph(ChartSetup chartSetup, ChartInfo graph)
        {
            var dataSource = chartSetup.DataSource as IList;

            foreach (var s in chartSetup.PrimarySeries)
            {
                dataSource.Add(s);
            }

            foreach (var s in chartSetup.SecondarySeries)
            {
                dataSource.Add(s);
            }

            chartSetup.PrimarySeries.Clear();
            chartSetup.SecondarySeries.Clear();

            foreach (var graphSeries in graph.Series)
            {
                var s = (chartSetup.DataSource as IList).Cast<SeriesInfo>().Where(serie => serie.Name == graphSeries.Name).First();
                Debug.Assert(s != null);

                if (graphSeries.ShowInSecondaryVerticalAxis)
                {
                    s.ShowInSecondaryVerticalAxis = true;
                    chartSetup.SecondarySeries.Add(s);
                }
                else
                {
                    s.ShowInSecondaryVerticalAxis = false;
                    chartSetup.PrimarySeries.Add(s);
                }

                dataSource.Remove(s);
            }

            chartSetup.Refresh();
        }

        private IList<ChartInfo> generateCharts()
        {
            var charts = new List<ChartInfo>();

            foreach (var tab in View.Tabs)
            {
                var chartSetUp = tab.Controls[0] as ChartSetup;
                var series = chartSetUp.PrimarySeries.Cast<SeriesInfo>().Union(chartSetUp.SecondarySeries.Cast<SeriesInfo>());
                var secondaryTitle = string.Join(Strings.SeriesHeaderSeparator, SeriesInfo.GetSeriesCollectionName(series));
                charts.Add(new ChartInfo(data.Title, secondaryTitle, series));
            }

            return charts;
        }
    }
}
