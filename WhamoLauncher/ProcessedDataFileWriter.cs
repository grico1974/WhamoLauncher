using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WhamoLauncher
{
    internal sealed class ProcessedDataFileWriter : IDisposable
    {
        private readonly StreamWriter writer;
        private static readonly string separator = "\t";
        bool disposed;

        public ProcessedDataFileWriter(string path)
        {
            writer = new StreamWriter(path, false, Encoding.Unicode);
        }

        public void Write(IEnumerable<SimulationDataBlock> blocks)
        {
            if (disposed)
                throw new ObjectDisposedException("writer");

            int dataCount = 0;
            string simulationTitle = string.Empty;
            string headers = string.Empty;
            string additionalHeaders = string.Empty;
            string units = string.Empty;
            
            foreach (var block in blocks)
            {
                headers = string.Concat(headers, headers.Length > 0 ? separator : string.Empty, string.Join(separator, block.Headers));
                additionalHeaders = string.Concat(additionalHeaders, additionalHeaders.Length > 0 ? separator : string.Empty, string.Join(separator, block.AdditionalHeaderInformation));
                units = string.Concat(units, units.Length > 0 ? separator : string.Empty, string.Join(separator, block.Units));
                dataCount = block.DataCount;
                simulationTitle = block.SimulationTitle;
            }

            writer.WriteLine(simulationTitle);
            writer.WriteLine(headers);
            writer.WriteLine(additionalHeaders);
            writer.WriteLine(units);

            for (int i = 0; i < dataCount; i++)
            {
                string data = string.Empty;

                foreach (var block in blocks)
                {
                    data = string.Concat(data, data.Length > 0 ? separator : string.Empty, string.Join(separator, block.GetValues(i)));
                }

                writer.WriteLine(data);
            }
        }

        public void Dispose()
        {
            dispose(true);
        }

        private void dispose(bool isSafe)
        {
            if (disposed)
                return;

            if (isSafe)
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Dispose();
                }

                disposed = true;
                GC.SuppressFinalize(this);
            }
        }

        ~ProcessedDataFileWriter()
        {
            dispose(false);
        }
    }
}
