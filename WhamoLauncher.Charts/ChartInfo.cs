using System;
using System.Collections.Generic;
using System.Linq;

namespace WhamoLauncher.Charts
{
    internal sealed class ChartInfo
    {
        public IEnumerable<SeriesInfo> Series { get; }
        public bool HasSecondaryVerticalAxis => Series.Any(s => !s.ShowInSecondaryVerticalAxis) &&
                                                Series.Any(s => s.ShowInSecondaryVerticalAxis);

        public ChartInfo(string primaryTitle, string secondaryTitle, IEnumerable<SeriesInfo> seriesInfo)
        {
            if (primaryTitle == null)
            {
                throw new ArgumentNullException(nameof(primaryTitle));
            }

            if (seriesInfo == null)
            {
                throw new ArgumentNullException(nameof(seriesInfo));
            }

            Series = seriesInfo.Select(s => s).ToList();
            PrimaryTitle = primaryTitle.Trim();
            SecondaryTitle = secondaryTitle ?? string.Empty;
        }

        public string PrimaryTitle { get; }
        public string SecondaryTitle { get; }
        public string GetHorizontalAxisTitle() => string.Join(Strings.AxisTitleSeparator, Series.Select(s => s.XUnitName).Distinct());
        public string GetPrimaryVerticalAxisTitle() => HasSecondaryVerticalAxis ? 
                                                       string.Join(Strings.AxisTitleSeparator,
                                                                   Series.Where(s => !s.ShowInSecondaryVerticalAxis)
                                                                         .Select(s => s.YUnitName)
                                                                         .Distinct()):
                                                       string.Join(Strings.AxisTitleSeparator, Series.Select(s => s.YUnitName).Distinct());

        public string GetSecondaryVerticalAxisTitle() => string.Join(Strings.AxisTitleSeparator, 
                                                                     Series.Where(s => s.ShowInSecondaryVerticalAxis)
                                                                           .Select(s => s.YUnitName)
                                                                           .Distinct());
    }
}
