using System;
using Android.OS;
using Java.Lang;

namespace Athela.Mealo.Droid.Extensions
{
    static class DateTimeExtensions
    {
        internal static long ToElapsedRealtime(this DateTime value)
        {
            var utcValue = value.ToUniversalTime();
            var diff = JavaSystem.CurrentTimeMillis() - SystemClock.ElapsedRealtime();
            return Convert.ToInt64((utcValue - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Add(TimeSpan.FromMilliseconds(-diff)).TotalMilliseconds) + 1000;
        }
    }
}