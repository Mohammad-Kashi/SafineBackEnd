using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Location.ValueObjects
{
    public class Time
    {
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public Time(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }
        public Time(DateTime time)
        {
            Hour = time.Hour;
            Minute = time.Minute;
        }
        public static bool operator >=(Time left, Time right)
        {
            if (left.Hour > right.Hour)
                return true;
            if (left.Hour < right.Hour)
                return false;
            if (left.Minute >= right.Minute)
                return true;
            return false;
        }
        public static bool operator <=(Time left, Time right)
        {
            if (left.Hour < right.Hour)
                return true;
            if (left.Hour > right.Hour)
                return false;
            if (left.Minute <= right.Minute)
                return true;
            return false;
        }
    }
}
