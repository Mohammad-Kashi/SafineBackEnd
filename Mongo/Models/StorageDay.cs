using Domain.Location.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Models
{
    public record StorageDay
    {
        public int Month { get; private set; }
        public int DayOfMonth { get; private set; }
        public StorageDay(Day day)
        {
            Month = day.Month;
            DayOfMonth = day.DayOfMonth;
        }
        public Day ToModel()
        {
            return new Day(Month, DayOfMonth);
        }
    }
}
