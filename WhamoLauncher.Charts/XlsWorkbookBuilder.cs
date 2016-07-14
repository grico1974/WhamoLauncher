using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using WhamoLauncher.Resources;
using Xls = Microsoft.Office.Interop.Excel;

namespace WhamoLauncher.Charts
{
    internal sealed class XlsWorkbookBuilder: IDisposable
    {
        public static FileInfo BuildExcelFile(string xlsFilePath, IEnumerable<ChartInfo> charts)
        {
            if (xlsFilePath == null)
            {
                throw new ArgumentNullException("xlsFilePath");
            }

            if (charts == null)
            {
                throw new ArgumentNullException("charts");
            }

            using (var builder = new XlsWorkbookBuilder(charts))
            {
                return builder.BuildFile(xlsFilePath);
            }
        }

        private bool disposed;
        private readonly IEnumerable<ChartInfo> graphs;
        private Xls.Application excelApplication;

        private XlsWorkbookBuilder(IEnumerable<ChartInfo> graphsInfo)
        {
            Debug.Assert(graphsInfo != null);
            graphs = graphsInfo;
            excelApplication = new Xls.Application();
            excelApplication.Visible = false;
            excelApplication.DisplayAlerts = false;
            excelApplication.ScreenUpdating = false;
        }

        public FileInfo BuildFile(string xlsFilePath)
        {
            Debug.Assert(!disposed);
            ThirdPartyResourcesProvider.CopyXlsTemplateTo(xlsFilePath);
            var workBook = excelApplication.Workbooks.Open(xlsFilePath);

            try
            {
                builWorkBook(workBook, graphs);
                workBook.Save();
                return new FileInfo(xlsFilePath);
            }
            finally
            {
                Marshal.FinalReleaseComObject(workBook);
            }
        }

        private void builWorkBook(Xls.Workbook workBook, IEnumerable<ChartInfo> graphs)
        {
            Xls.Chart graphTemplate = workBook.Sheets[1];
            Xls.Worksheet dataSheet = workBook.Sheets[2];
            int chartsCounter = 1;
            int currentDataColumn = 1;

            try
            {
                foreach (var graph in graphs)
                {
                    int dataColumnsWritten;
                    graphTemplate.Copy(dataSheet);
                    workBook.Sheets[workBook.Sheets.Count - 1].Name = string.Format(Strings.ExcelChartSheetName, chartsCounter++);
                    buildXlsChart(workBook.Sheets[workBook.Sheets.Count - 1], dataSheet, graph, currentDataColumn, out dataColumnsWritten);
                    currentDataColumn += dataColumnsWritten;
                }

                graphTemplate.Delete();
            }
            finally
            {
                if (dataSheet != null)
                {
                    Marshal.FinalReleaseComObject(dataSheet);
                }

                Marshal.FinalReleaseComObject(graphTemplate);
            }
        }

        private void buildXlsChart(Xls.Chart chart, Xls.Worksheet dataSheet, ChartInfo graph, int currentDataColumn, out int dataColumnsWritten)
        {
            chart.ChartTitle.Text = string.Format(Strings.ChartTitle, graph.PrimaryTitle, graph.SecondaryTitle);
            dataColumnsWritten = 0;
            double[,] xValues = null, yValues = null;
            int seriesCounter = 0;

            foreach (var s in graph.Series)
            {
                if (seriesCounter == 0)
                {
                    dataSheet.Cells[2, currentDataColumn].Value = s.XUnitName;
                    xValues = ConvertToTwoDimensionalArray(s.XValues);
                    dataSheet.Range[dataSheet.Cells[3, currentDataColumn], dataSheet.Cells[3 + xValues.Length - 1, currentDataColumn]].Value2 = xValues;
                    dataColumnsWritten += 1;
                }

                dataSheet.Cells[1, currentDataColumn + dataColumnsWritten].Value = s.Name;
                dataSheet.Cells[2, currentDataColumn + dataColumnsWritten].Value = s.YUnitName;
                yValues = ConvertToTwoDimensionalArray(s.YValues);
                dataSheet.Range[dataSheet.Cells[3, currentDataColumn + dataColumnsWritten], dataSheet.Cells[3 + yValues.Length - 1, currentDataColumn + dataColumnsWritten]].Value2 = yValues;
                dataColumnsWritten += 1;
                seriesCounter++;
            }

            if (seriesCounter > 0)
            {
                chart.SetSourceData(Source: dataSheet.Range[dataSheet.Cells[3, currentDataColumn], dataSheet.Cells[3 + xValues.Length - 1, currentDataColumn + dataColumnsWritten - 1]]);
                Xls.SeriesCollection collection = chart.SeriesCollection();
                Xls.Axes axes = null;

                try
                {
                    for (int i = 0; i < collection.Count; i++)
                    {
                        collection.Item(i + 1).Name = graph.Series.ElementAt(i).Name;

                        if (graph.Series.ElementAt(i).ShowInSecondaryVerticalAxis && graph.HasSecondaryVerticalAxis)
                        {
                            collection.Item(i + 1).AxisGroup = Xls.XlAxisGroup.xlSecondary;
                        }
                    }

                    axes = chart.Axes();
                    axes.Item(Xls.XlAxisType.xlCategory, Xls.XlAxisGroup.xlPrimary).AxisTitle.Text = graph.GetHorizontalAxisTitle();
                    axes.Item(Xls.XlAxisType.xlValue, Xls.XlAxisGroup.xlPrimary).AxisTitle.Text = graph.GetPrimaryVerticalAxisTitle();

                    if (graph.HasSecondaryVerticalAxis)
                    {
                        axes.Item(Xls.XlAxisType.xlValue, Xls.XlAxisGroup.xlSecondary).HasTitle = true;
                        axes.Item(Xls.XlAxisType.xlValue, Xls.XlAxisGroup.xlSecondary).AxisTitle.Text = graph.GetSecondaryVerticalAxisTitle();
                    }
                }
                finally
                {
                    if (axes != null)
                    {
                        Marshal.FinalReleaseComObject(axes);
                    }

                    Marshal.FinalReleaseComObject(collection);
                }
            }
        }

        private static double[,] ConvertToTwoDimensionalArray(IEnumerable<double> values)
        {
            var valuesArray = values.ToArray();
            var arr = new double[valuesArray.Length, 1];

            for (int i = 0; i < valuesArray.Length; i++)
            {
                arr[i, 0] = valuesArray[i];
            }

            return arr;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool safe)
        {
            if (disposed)
            {
                return;
            }

            if (excelApplication != null)
            {
                excelApplication.ScreenUpdating = true;
                excelApplication.Quit();
                //GC.Collect();
                //GC.WaitForPendingFinalizers(); These calls don't seem to be necessary, excel process is freed correctly.
                Marshal.FinalReleaseComObject(excelApplication);
            }

            disposed = true;
        }

        ~XlsWorkbookBuilder()
        {
            dispose(false);
        }
    }
}
