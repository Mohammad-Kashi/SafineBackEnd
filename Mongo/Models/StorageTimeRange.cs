using Domain.Location.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Models
{
    public record StorageTimeRange
    {
        public int StartHour { get; init; }
        public int StartMinute { get; init; }
        public int EndHour { get; init; }
        public int EndMinute { get; init; }
        public StorageTimeRange(TimeRange timeRange)
        {
            StartHour = timeRange.StartTime.Hour;
            StartMinute = timeRange.StartTime.Minute;
            EndHour = timeRange.EndTime.Hour;
            EndMinute = timeRange.EndTime.Minute;
        }
        public TimeRange ToModel()
        {
            return new TimeRange(new Time(StartHour, StartMinute), new Time(EndHour, EndMinute));
        }
    }
}
