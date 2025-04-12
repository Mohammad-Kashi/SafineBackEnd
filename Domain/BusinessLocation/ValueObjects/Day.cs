using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Location.ValueObjects
{
    public class Day
    {
        public int Month { get; private set; }
        public int DayOfMonth { get; private set; }
        public Day(int month, int dayOfMonth)
        {
            Month = month;
            DayOfMonth = dayOfMonth;
        }
    }
}
