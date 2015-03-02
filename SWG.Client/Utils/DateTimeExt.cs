using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Utils
{
    public static class DateTimeExt
    {
        private static DateTime _Epoc = new DateTime(1970, 1, 1);
        private static UInt64 _StoredTime;

        private static System.Threading.Timer _Timer;

        static DateTimeExt()
        {
            _SetStoredTime(null);
            _Timer = new System.Threading.Timer(_SetStoredTime, null, 1000,1000);
        }

        private static void _SetStoredTime(object sync)
        {
            _StoredTime = DateTime.UtcNow.GetMilliseconds();
        }

        public static UInt64 GetMilliseconds(this DateTime Time)
        {
            return Convert.ToUInt64((Time - _Epoc).TotalMilliseconds);
        }

        public static UInt64 GetStoredMilliseconds()
        {
            return _StoredTime;
        }
    }
}
