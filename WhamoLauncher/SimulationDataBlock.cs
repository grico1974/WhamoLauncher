using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WhamoLauncher
{
    internal sealed class SimulationDataBlock
    {
        private string title;
        private readonly List<string> headers;
        private readonly List<string> additionalHeaders;
        private readonly List<string> units;
        private readonly List<List<double>> values;

        public static SimulationDataBlock ProcessRawBlockData(IEnumerable<string> block, bool discardTimeInformation)
        {
            return new RawDataProcessor(block).ExtractData(discardTimeInformation);
        }

        private SimulationDataBlock(string title, List<string> headers, List<string> additionalHeaders, List<string> units, List<List<double>> values)
        {
            this.title = title;
            this.headers = headers;
            this.additionalHeaders = additionalHeaders;
            this.units = units;

            if (units.Count != headers.Count)
            {
                throw new InvalidDataException(Strings.OutFileFormatError);
            }

            this.values = values;
        }

        public string SimulationTitle { get { return title; } }

        public int DataCount { get { return values[0].Count; } }

        public IEnumerable<string> Headers { get { return headers; } }

        public IEnumerable<string> AdditionalHeaderInformation { get { return additionalHeaders; } }

        public IEnumerable<string> Units { get { return units; } }

        public IEnumerable<double> GetValues(string header)
        {
            var index = headers.IndexOf(header);
            Debug.Assert(index > -1);

            if (index > -1)
            {
                foreach (var data in values[index])
                {
                    yield return data;
                }
            }
        }

        public IEnumerable<double> GetValues(int row)
        {
            foreach (var column in values)
            {
                yield return column[row];
            }
        }

        private class RawDataProcessor
        {
            private const string timeWhamoHeader = "TIME";
            private const string dischargeWhamoHeader = "DISCHARGE";
            private const string energyElevationWhamoHeader = "ENERGY ELEV.";
            private const string pressureHeadWhamoHeader = "PRES. HEAD";
            private const string piezometricElevationWhamoHeader = "PIEZ. ELEV.";
            private const string speedWhamoHeader = "SPEED";
            private const string torqueWhamoHeader = "TORQUE";
            private const string powerWhamoHeader = "POWER";
            private const string gateOpeningWhamoHeader = "GATE OPENING";
            private const string waterSurfaceElevationWhamoHeader = "W.S. ELEV.";
            private const string titleHeader = " TITLED:";
            private const string distanceWhamoUnitName = "(FEET)";
            private const string flowWhamoUnitName = "(CFS)";
            private const string torqueWhamoUnitName = "(FT-KIP)";
            private const string powerWhamoUnitName = "(HP)";
            private const string timeWhamoUnitName = "(SEC.)";
            private const string oneWhiteSpace = " ";
            private const string twoWhiteSpaces = "  ";
            private const string threeWhiteSpaces = "   ";
            private const int titleInformationRow = 0;
            private const int headerInformationRow = 1;
            private const int unitInformationRow = 3;
            private const int timeInformationColumn = 0;
            private static readonly int outputDecimalPlaces = 3;

            private readonly List<string> block;
            private List<Func<double, double>> unitConverters;

            public RawDataProcessor(IEnumerable<string> rawBlock)
            {
                Debug.Assert(rawBlock != null);
                block = rawBlock.ToList();
            }

            public SimulationDataBlock ExtractData(bool discardTimeInformation)
            {
                var title = ExtractTitle();
                var headers = ExtractHeaders();
                var additionalHeaders = ExtractAdditionalHeaderInformation();
                
                if (!discardTimeInformation)
                {
                    additionalHeaders.Insert(0, string.Empty); //time column has no additional header information.
                }

                var units = ExtractUnits();
                var values = ExtractValues(units.Count);

                if (discardTimeInformation)
                {
                    removeDuplicateTimeInformation(headers, units, values);
                }

                translateTimeHeader(headers);
                return new SimulationDataBlock(title, headers, additionalHeaders, units, values);
            }

            private void removeDuplicateTimeInformation(List<string> headers, List<string> units, List<List<double>> values)
            {
                if (string.Compare(headers[timeInformationColumn], timeWhamoHeader, false, CultureInfo.InvariantCulture) == 0)
                {
                    headers.RemoveAt(timeInformationColumn);
                    units.RemoveAt(timeInformationColumn);
                    values.RemoveAt(timeInformationColumn);
                }
            }

            private void translateTimeHeader(List<string> headers)
            {
                if (headers.Count > 0 &&
                    string.Compare(headers[timeInformationColumn], timeWhamoHeader, false, CultureInfo.InvariantCulture) == 0)
                {
                    headers[timeInformationColumn] = Strings.TranslatedTimeHeader;
                }
            }

            private string ExtractTitle()
            {
                return processTitle(block[titleInformationRow]);
            }

            private List<string> ExtractHeaders()
            {
                return processHeaders(block[headerInformationRow]);
            }

            private List<string> ExtractAdditionalHeaderInformation()
            {
                return processHeaders(block[headerInformationRow + 1]).Select(h => translateAdditionalHeaderInformation(h)).ToList();
            }

            private List<string> ExtractUnits()
            {
                var units = processUnits(block[unitInformationRow]);
                unitConverters = new List<Func<double, double>>(units.Count);

                foreach (var unit in units)
                {
                    unitConverters.Add(GetUnitConverter(unit));
                }

                return units.Select(u => translateUnit(u)).ToList();
            }

            private List<List<double>> ExtractValues(int columnCount)
            {
                return processData(columnCount);
            }

            private string processTitle(string titleData)
            {
                if (titleData.StartsWith(titleHeader))
                {
                    return titleData.Substring(8).Trim();
                }
                else
                    throw new InvalidDataException(Strings.OutFileFormatError);
            }

            private List<string> processHeaders(string headerData)
            {
                while (headerData.IndexOf(threeWhiteSpaces) > -1)
                {
                    headerData = headerData.Replace(threeWhiteSpaces, twoWhiteSpaces);
                }

                return headerData.Split(new[] { twoWhiteSpaces }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(h => h.Trim()).ToList();
            }

            private List<string> processUnits(string unitData)
            {
                while (unitData.IndexOf(threeWhiteSpaces) > -1)
                {
                    unitData = unitData.Replace(threeWhiteSpaces, twoWhiteSpaces);
                }

                return unitData.Split(new[] { twoWhiteSpaces }, StringSplitOptions.RemoveEmptyEntries)
                               .Select(u => u.Trim()).ToList();
            }

            private List<List<double>> processData(int columnCount)
            {
                var datum = new List<List<double>>();

                for (int i = 0; i < columnCount; i++)
                {
                    datum.Add(new List<double>());
                }

                foreach (var dataRecord in block.Skip(unitInformationRow + 1))
                {
                    var counter = 0;

                    foreach (var data in dataRecord.Split(new[] { oneWhiteSpace }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        double value = 0;

                        if (double.TryParse(data, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                        {
                            datum[counter].Add(unitConverters[counter](value));
                            counter++;
                        }
                        else
                        {
                            throw new InvalidDataException();
                        }
                    }
                }

                return datum;
            }

            private Func<double, double> GetUnitConverter(string unit)
            {
                switch (unit)
                {
                    case distanceWhamoUnitName:
                        {
                            return x => Math.Round(0.3048 * x, outputDecimalPlaces); //to meters
                        }
                    case flowWhamoUnitName:
                        {
                            return x => Math.Round(0.02832 * x, outputDecimalPlaces); // to m^3/s
                        }
                    case torqueWhamoUnitName:
                        {
                            return x => Math.Round(1.35582 * x, outputDecimalPlaces); //to kN·m
                        }
                    case powerWhamoUnitName:
                        {
                            return x => Math.Round(0.7457 * x, outputDecimalPlaces); //to kW
                        }
                    default:
                        {
                            return (x) => Math.Round(x, outputDecimalPlaces);
                        }
                }
            }

            private string translateUnit(string unit)
            {
                switch (unit)
                {
                    case distanceWhamoUnitName:
                        {
                            return Strings.TranslatedDistanceUnit;
                        }
                    case flowWhamoUnitName:
                        {
                            return Strings.TranslatedFlowUnit;
                        }
                    case torqueWhamoUnitName:
                        {
                            return Strings.TranslatedTorqueUnit;
                        }
                    case powerWhamoUnitName:
                        {
                            return Strings.TranslatedPowerUnit;
                        }
                    case timeWhamoUnitName:
                        {
                            return Strings.TranslatedTimeUnit;
                        }
                    default:
                        {
                            return unit;
                        }
                }
            }

            private string translateAdditionalHeaderInformation(string header)
            {
                switch (header)
                {
                    case dischargeWhamoHeader:
                    {
                        return Strings.TranslatedDischargeHeader;
                    }
                    case energyElevationWhamoHeader:
                    {
                        return Strings.TranslatedEnergyElevationHeader;
                    }
                    case pressureHeadWhamoHeader:
                    {
                        return Strings.TranslatedPressureHeader;
                    }
                    case piezometricElevationWhamoHeader:
                    {
                        return Strings.TranslatedPiezometricHeader;
                    }
                    case speedWhamoHeader:
                    {
                        return Strings.TranslatedSpeedHeader;
                    }
                    case powerWhamoHeader:
                    {
                        return Strings.TranslatedPowerHeader;
                    }
                    case torqueWhamoHeader:
                    {
                        return Strings.TranslatedTorqueHeader;
                    }
                    case gateOpeningWhamoHeader:
                    {
                        return Strings.TranslatedOpeningHeader;
                    }
                    case waterSurfaceElevationWhamoHeader:
                    {
                        return Strings.TranslatedWaterElevationHeader;
                    }
                    default:
                    {
                        return header;
                    }
                }
            }
        }
    }
}
