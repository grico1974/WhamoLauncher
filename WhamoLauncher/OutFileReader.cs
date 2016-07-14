using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace WhamoLauncher
{
    internal sealed class OutFileReader: IDisposable
    {
        private readonly Reader reader;
        private bool disposed;

        public OutFileReader(string outFilePath)
        {
            reader = new Reader(outFilePath);

            if (reader.EndOfStream)
            {
                throw new ArgumentException(string.Format(Strings.OutFileEmptyError, outFilePath));
            }
        }

        public IEnumerable<SimulationDataBlock> ProcessOutputData()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("reader");
            }

            if (reader.EndOfStream)
            {
                throw new InvalidOperationException(string.Format(Strings.MultipleInvocationsError, nameof(ProcessOutputData) /*"OutFileReader.ProcessOutputData()"*/));
            }

            var blocks = reader.ReadSimulationDataBlocks().ToList();
            Debug.Assert(blocks.Count > 0);
            var processedBlocks = new List<SimulationDataBlock>();
            processedBlocks.Add(SimulationDataBlock.ProcessRawBlockData(blocks.First(), false));

            foreach (var block in blocks.Skip(1))
            {
                processedBlocks.Add(SimulationDataBlock.ProcessRawBlockData(block, true));
            }

            return processedBlocks;
        }

        public void Dispose()
        {
            dispose(true);
        }

        private void dispose(bool isSafe)
        {
            if (disposed)
            {
                return;
            }

            if (isSafe)
            {
                if (reader != null)
                {
                    reader.Dispose();
                }

                disposed = true;
                GC.SuppressFinalize(this);
            }
        }

        ~OutFileReader()
        {
            dispose(false);
        }

        private class Reader: IDisposable
        {
            private const string dataBlockHeader = "\fSIMULATION OUTPUT";
            private const string dataBlockPageSeparator = "\f";
            private const string endOfOutFile = "   GOODBYE";
            private const int headerHeight = 7;

            private readonly StreamReader stream;
            private bool disposed;

            public bool EndOfStream { get { return stream.EndOfStream; } }

            public Reader(string outFilePath)
            {
                stream = new StreamReader(outFilePath, Encoding.UTF7, true);
            }

            public IEnumerable<IEnumerable<string>> ReadSimulationDataBlocks()
            {
                advanceToFirstDataBlock();
                var block = readSimulationDataBlock().ToList();

                while (block.Count != 0)
                {
                    yield return block;
                    block = readSimulationDataBlock().ToList();
                }
            }

            private void advanceToFirstDataBlock()
            {
                string line;

                do
                {
                    line = stream.ReadLine();
                }
                while (!line.StartsWith(dataBlockHeader));

                if (stream.EndOfStream)
                {
                    throw new InvalidDataException(Strings.OutFileFormatError); //no data blocks
                }
            }

            private IEnumerable<string> readSimulationDataBlock()
            {
                //TODO Presently does not read warnings: air separation, surge tank low, etc.
                if (stream.EndOfStream)
                    yield break;

                foreach (var header in readHeaderInformation())
                {
                    yield return header;
                }

                string line = stream.ReadLine();
                
                while (!line.StartsWith(dataBlockHeader))
                {
                    if (stream.EndOfStream)
                    {
                        throw new InvalidDataException(Strings.OutFileFormatError); //missing goodbye
                    }

                    /*if (line.StartsWith(dataBlockHeader))
                    {
                        yield break;
                    }*/

                    if (line == dataBlockPageSeparator)
                    {
                        for (int i = 0; i < headerHeight; i++)
                        {
                            line = stream.ReadLine(); //skip 6 lines of redundant header information and read first data line.
                            
                            if (line.StartsWith(endOfOutFile))
                            {
                                yield break;
                            }

                            else if (stream.EndOfStream)
                            {
                                throw new InvalidDataException(Strings.OutFileFormatError); //missing goodbye
                            }
                        }
                    }

                    if (line.Length != 0)
                    {
                        yield return line;
                    }

                    line = stream.ReadLine();
                }

                yield break;
            }

            private IEnumerable<string> readHeaderInformation()
            {
                stream.ReadLine(); //skip underlining
                stream.ReadLine(); //skip empty row
                stream.ReadLine(); //skip time stamp
                yield return stream.ReadLine(); //project title
                stream.ReadLine(); //skip empty row
                yield return stream.ReadLine(); //headers
                yield return stream.ReadLine(); ; //headers
                yield return stream.ReadLine(); //headers
                stream.ReadLine(); //skip empty row
                stream.ReadLine(); //skip underlining
                //TODO Check if there are warnings and act in consequence
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
                    if (stream != null)
                    {
                        stream.Dispose();
                    }

                    disposed = true;
                    GC.SuppressFinalize(this);
                }
            }

            ~Reader()
            {
                dispose(false);
            }
        }
    }
}
