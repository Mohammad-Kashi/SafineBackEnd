using Domain.Location;
using Domain.Location.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mongo.Models
{
    public record StorageBusinessLocation
    {
        public StorageBusinessLocation(BusinessLocation location)
        {
            BusinessId = string.IsNullOrEmpty(location.Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(location.Id);
            ManagerId = location.ManagerId;
            Name = location.Name;
            WorkingShifts = location.WorkingShifts.Select(t => new StorageTimeRange(t));
            AverageCustomerTimeMinutes = location.AverageCustomerTimeMinutes;
            BusinessTags = location.BusinessTags;
            Description = location.Description;
            WorkingDays = location.WorkingDays;
            Location = location.Location;
            OffDays = location.OffDays.Select(t => new StorageDay(t));
            PeopleInLine = location.PeopleInLine;
            SearchText = location.Name + " " + location.Description + " " + string.Join(" ", location.BusinessTags);
        }
        public BusinessLocation ToModel()
        {
            return new BusinessLocation(BusinessId.ToString(), ManagerId, Name, WorkingShifts.Select(t => t.ToModel()).ToList(), AverageCustomerTimeMinutes, BusinessTags.ToList(), Description, WorkingDays.ToList(), Location, OffDays.Select(t => t.ToModel()).ToList(), PeopleInLine.ToList());
        }
        [BsonId]
        public ObjectId BusinessId { get; init; }
        public string ManagerId { get; init; }
        public string Name { get; init; }
        public IEnumerable<StorageTimeRange> WorkingShifts { get; init; }
        public int AverageCustomerTimeMinutes { get; init; }
        public IEnumerable<string> BusinessTags { get; init; }
        public string Description { get; init; }
        public IEnumerable<int> WorkingDays { get; init; }
        public string Location { get; init; }
        public IEnumerable<StorageDay> OffDays { get; init; }
        public IEnumerable<string> PeopleInLine { get; init; }
        public string SearchText { get; init; }
    }
}
