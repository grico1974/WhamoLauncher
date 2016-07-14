using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WhamoLauncher.Charts
{
    internal static class GraphTemplateStream
    {
        public static IEnumerable<ChartInfo> Read(string path, OutputData data)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return new _GraphTemplateStream(path).Read(data);
        }

        public static void Write(string path, IEnumerable<ChartInfo> charts)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (charts == null)
            {
                throw new ArgumentNullException(nameof(charts));
            }

            new _GraphTemplateStream(path).Write(charts);
        }

        private class _GraphTemplateStream
        {
            private readonly string path;

            public _GraphTemplateStream(string templateFilePath)
            {
                path = templateFilePath;
            }

            public IEnumerable<ChartInfo> Read(OutputData data)
            {
                var series = OutputData.GetAllSeries(data);

                try
                {
                    using (var file = new FileStream(path, FileMode.Open))
                    {
                        using (var reader = new StreamReader(file))
                        {
                            var graphs = new List<ChartInfo>();
                            var title = data.Title;
                            var line = reader.ReadLine();

                            if (line != null && line != Strings.TemplateGraphTag)
                            {
                                throw new FormatException();
                            }

                            bool endOfStream = reader.EndOfStream;

                            while (!endOfStream)
                            {
                                line = reader.ReadLine();

                                if (line != Strings.TemplatePrimaryTag)
                                {
                                    throw new FormatException();
                                }

                                var primarySeries = readSeries(reader, series, out endOfStream);
                                var secondarySeries = readSeries(reader, series, out endOfStream);

                                foreach (var s in primarySeries)
                                {
                                    s.ShowInSecondaryVerticalAxis = false;
                                }

                                foreach (var s in secondarySeries)
                                {
                                    s.ShowInSecondaryVerticalAxis = true;
                                }

                                var chartSeries = primarySeries.Union(secondarySeries);
                                graphs.Add(new ChartInfo(title, SeriesInfo.GetSeriesCollectionName(chartSeries), chartSeries));
                            }

                            return graphs;
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    throw new FormatException();
                }
            }

            private IEnumerable<SeriesInfo> readSeries(StreamReader reader, IEnumerable<SeriesInfo> series, out bool endOfStream)
            {
                endOfStream = false;
                var line = string.Empty;
                var recognizedSeries = new List<SeriesInfo>();

                while (!reader.EndOfStream)
                {
                    var seriesName = reader.ReadLine();

                    if (seriesName == Strings.TemplateGraphTag || seriesName == Strings.TemplateSecondaryTag)
                    {
                        break;
                    }

                    var s = series.Where(sr => sr.Name == seriesName).FirstOrDefault();

                    if (s != null)
                    {
                        recognizedSeries.Add(s);
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }

                if (reader.EndOfStream)
                {
                    endOfStream = true;
                }

                return recognizedSeries;
            }

            public void Write(IEnumerable<ChartInfo> charts)
            {
                try
                {
                    using (var file = new FileStream(path, FileMode.Create))
                    {
                        using (var writer = new StreamWriter(file))
                        {
                            for (int i = 0; i < charts.Count(); i++)
                            {
                                writer.WriteLine(Strings.TemplateGraphTag);
                                writer.WriteLine(Strings.TemplatePrimaryTag);
                                var graph = charts.ElementAt(i);

                                foreach (var s in graph.Series.Where(s => !s.ShowInSecondaryVerticalAxis))
                                {
                                    writer.WriteLine(s.Name);
                                }

                                writer.WriteLine(Strings.TemplateSecondaryTag);

                                foreach (var s in graph.Series.Where(s => s.ShowInSecondaryVerticalAxis))
                                {
                                    writer.WriteLine(s.Name);
                                }
                            }

                            writer.Flush();
                        }
                    }
                }
                catch (IOException)
                {
                    //swallow it. Not important if template can't be generated.
                }
            }
        }
    }
}