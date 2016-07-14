using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WhamoLauncher.Charts
{
    internal sealed class SeriesInfo
    {
        public SeriesInfo(string name, string xUnitName, string yUnitName, IEnumerable<double> xValues,
                               IEnumerable<double> yValues, bool showInSecondaryVerticalAxis = false)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (xUnitName == null)
            {
                throw new ArgumentNullException(nameof(xUnitName));
            }

            if (yUnitName == null)
            {
                throw new ArgumentNullException(nameof(yUnitName));
            }

            if (xValues == null)
            {
                throw new ArgumentNullException(nameof(xValues));
            }

            if (yValues == null)
            {
                throw new ArgumentNullException(nameof(yValues));
            }

            Name = name.Trim();
            XUnitName = xUnitName.Trim();
            YUnitName = yUnitName.Trim();
            this.xValues = xValues.ToArray();
            this.yValues = yValues.ToArray();
            ShowInSecondaryVerticalAxis = showInSecondaryVerticalAxis;
        }

        private readonly double[] xValues, yValues;
        public IEnumerable<double> XValues => xValues;
        public IEnumerable<double> YValues => yValues;
        public bool ShowInSecondaryVerticalAxis { get; set; }
        public string XUnitName { get; }
        public string YUnitName { get; }
        public string Name { get; }

        public static string GetSeriesCollectionName(IEnumerable<SeriesInfo> series) =>
                             string.Join(Strings.SeriesHeaderSeparator,
                                         series.Select(s => s.Name.Split(new string[] { Strings.SeriesHeaderSeparator }, 
                                                                         StringSplitOptions.RemoveEmptyEntries).Last()).Distinct());

        [DebuggerStepThrough]
        public override string ToString() => Name;
    }
}
