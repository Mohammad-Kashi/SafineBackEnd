using Domain.Antipattern;
using Domain.IRepository;
using Domain.Location;
using Mongo.Dtos;
using Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Repository
{
    public class QueryRepository : IQueryRepository
    {
        private readonly IMongoCollection<StorageBusinessLocation> _location;
        public QueryRepository(IMongoDatabase database)
        {
            _location = database.GetCollection<StorageBusinessLocation>("StorageBusinessLocation");
        }

        public async Task<IEnumerable<BusinessLocation>> GetAllBusinessLocationsBySearch(BusinessSearchArg searchArg)
        {

            var query = new List<BsonDocument>()
            {
                new BsonDocument("$match", new BsonDocument("SearchText", new BsonDocument("$regex", searchArg.SearchText)))
            };
            if (searchArg.Tags.Any())
                query.Add(new BsonDocument("$match", new BsonDocument("$and", new BsonArray(searchArg.Tags.Select(tag => new BsonDocument("BusinessTags", tag))))));
            var result = await _location.Aggregate<StorageBusinessLocation>(query).ToListAsync();
            if (result is null || !result.Any() || string.IsNullOrEmpty(result.First().Name))
                return new List<BusinessLocation>();
            return result.Select(t => t.ToModel());
        }

        public async Task<BusinessLocation> GetBusinessLocationById(string locationId)
        {
            var query = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("_id", ObjectId.Parse(locationId)))
            };
            var result = await _location.Aggregate<StorageBusinessLocation>(query).FirstOrDefaultAsync();
            return result?.ToModel();
        }

        public async Task<IEnumerable<BusinessLocation>> GetBusinessLocationsByManagerId(string managerId)
        {
            var query = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("ManagerId", managerId))
            };
            var result = await _location.Aggregate<StorageBusinessLocation>(query).ToListAsync();
            if (result is null || !result.Any() || string.IsNullOrEmpty(result.First().Name))
                return new List<BusinessLocation>();
            return result.Select(t => t.ToModel());
        }

        public async Task<string> GetFirstInLine(string locationId, string managerId)
        {
            var query = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "_id", ObjectId.Parse(locationId) },
                    { "ManagerId", managerId }
                }),
                new BsonDocument("$project", new BsonDocument
                {
                    { "_id", 0 },
                    { "Result", new BsonDocument("$first", "$PeopleInLine") }
                })
            };
            var result = await _location.Aggregate<LazyStringDto>(query).FirstOrDefaultAsync();
            return result?.Result;
        }

        public async Task<List<string>> GetTags()
        {
            var query = new BsonDocument[]
            {
                new BsonDocument("$unwind", new BsonDocument
                {
                    { "path", "$BusinessTags" },
                    { "preserveNullAndEmptyArrays", false },
                }),
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$BusinessTags" },
                    { "count", new BsonDocument("$sum", 1) },
                }),
                new BsonDocument("$sort", new BsonDocument("count", -1)),
                new BsonDocument("$project", new BsonDocument
                {
                    { "_id", 0 },
                    { "Result", "$_id" }
                })
            };
            var result = await _location.Aggregate<LazyStringDto>(query).ToListAsync();
            if (result is null || !result.Any() || string.IsNullOrEmpty(result.First().Result))
                return new List<string>();
            return result.Select(t => t.Result).ToList();
        }

        public async Task<int> TimeRemaining(string locationId, string managerId)
        {
            var query = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument("_id", ObjectId.Parse(locationId))),
                new BsonDocument("$project", new BsonDocument
                {
                    { "_id", 0 },
                    { "NumberInLine", new BsonDocument("$indexOfArray", new BsonArray
                    {
                        "$PeopleInLine",
                        managerId
                    }) },
                    { "TimeUnit", "$AverageCustomerTimeMinutes" }
                })
            };
            var result = await _location.Aggregate<TimeRemainingDto>(query).FirstOrDefaultAsync();
            if (result is null)
                return -1;
            return result.NumberInLine * result.TimeUnit;
        }
    }
}
