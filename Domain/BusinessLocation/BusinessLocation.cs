using Domain.Location.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Location
{
    public class BusinessLocation
    {
        public BusinessLocation(string id, string managerId, string name, List<TimeRange> workingShifts, int averageCustomerTimeMinutes, List<string> businessTags, string description, List<int> workingDays, string location, List<Day> offDays, List<string> peopleInLine)
        {
            Id = id;
            ManagerId = managerId;
            Name = name;
            WorkingShifts = workingShifts;
            AverageCustomerTimeMinutes = averageCustomerTimeMinutes;
            BusinessTags = businessTags;
            Description = description;
            WorkingDays = workingDays;
            Location = location;
            OffDays = offDays;
            PeopleInLine = peopleInLine;
        }

        public string Id { get; private set; }
        public string ManagerId { get; private set; }
        public string Name { get; private set; }
        public List<TimeRange> WorkingShifts { get; private set; }
        public int AverageCustomerTimeMinutes { get; private set; }
        public List<string> BusinessTags { get; private set; }
        public string Description { get; private set; }
        public List<int> WorkingDays { get; private set; }
        public string Location { get; private set; }
        public List<Day> OffDays { get; private set; }
        public List<string> PeopleInLine { get; private set; }
    }
}
