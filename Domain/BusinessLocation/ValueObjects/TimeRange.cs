using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Location.ValueObjects
{
    public class TimeRange
    {
        public Time StartTime { get; private set; }
        public Time EndTime { get; private set; }
        public TimeRange(Time startTime, Time endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
        public bool IsInTimeRange(Time time)
        {
            if (time >= StartTime && time <= EndTime)
                return true;
            return false;
        }
    }
}
