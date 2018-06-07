using System;

namespace Athela.Mealo.Core.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToFormattedCookingTime(this TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes < 1)
            {
                return timeSpan.Seconds + "s";
            }

            var minutes = timeSpan.Minutes + (timeSpan.Seconds >= 30 ? 1 : 0);

            if (timeSpan.TotalHours >= 1)
            {
                return $"{timeSpan.TotalHours:0}h {minutes:0}m";
            }

            return $"{minutes:0}m";
        }
    }
}
