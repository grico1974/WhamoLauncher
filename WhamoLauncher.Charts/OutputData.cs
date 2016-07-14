using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WhamoLauncher.Charts
{
    internal sealed class OutputData
    {
        public static IEnumerable<SeriesInfo> GetAllSeries(OutputData data) => 
            data.Headers.Skip(1)
                .Select((header, index) => new SeriesInfo(header, data.Headers.ElementAt(0), data.Units.ElementAt(index + 1),
                                                          data.Datum.ElementAt(0), data.Datum.ElementAt(index + 1)));

        public static OutputData GetData(string path) => new OutputData(path);

        private IList<List<double>> datum;

        private OutputData(string path)
        {
            Debug.Assert(path != null);
            var reader = new StreamReader(path);

            try
            {
                Title = reader.ReadLine();
                var headers = reader.ReadLine().Split('\t');
                var headerAdditionalInfo = reader.ReadLine().Split('\t');
                Headers = headers.Select((s, index) => headerAdditionalInfo[index].Length > 0 ? 
                                                       string.Format(Strings.SeriesHeaderName, s, Strings.SeriesHeaderSeparator, headerAdditionalInfo[index]) : s)
                                 .ToList();
                Units = reader.ReadLine().Split('\t').ToList();
                readDatum(reader);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(Strings.FormatExceptionMessage, ex);
            }
            finally
            {
                reader.Dispose();
            }
        }

        public string Title { get; }
        public IEnumerable<string> Headers { get;}
        public IEnumerable<string> Units { get; }
        public IEnumerable<double> GetDatum(int headerIndex) => datum.ElementAt(headerIndex);
        public IEnumerable<IEnumerable<double>> Datum => datum;

        private void readDatum(StreamReader reader)
        {
            datum = new List<List<double>>();

            foreach (var header in Headers)
            {
                datum.Add(new List<double>());
            }

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                int index = 0;

                foreach (var value in line.Split('\t'))
                {
                    datum[index].Add(double.Parse(value));
                    index++;
                }
            }
        }
    }
}
