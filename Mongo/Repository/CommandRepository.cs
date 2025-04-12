using Domain.IRepository;
using Domain.Location;
using Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Repository
{
    public class CommandRepository : ICommandRepository
    {
        private readonly IClientSessionHandle _session;
        private readonly IMongoCollection<StorageBusinessLocation> _location;
        public CommandRepository(IMongoDatabase database, ITransaction transaction)
        {
            _location = database.GetCollection<StorageBusinessLocation>("StorageBusinessLocation");
            _session = transaction.CurrentSession as IClientSessionHandle;
        }

        public async Task AddPersonToList(string locationId, string personId)
        {
            var filterBuilder = Builders<StorageBusinessLocation>.Filter;
            var filter = filterBuilder.Empty;
            filter &= filterBuilder.Eq("_id", ObjectId.Parse(locationId));
            filter &= filterBuilder.Not(filterBuilder.Eq("PeopleInLine", personId));
            var update = Builders<StorageBusinessLocation>.Update
                .Push(nameof(StorageBusinessLocation.PeopleInLine), personId);
            var result = await _location.UpdateOneAsync(_session, filter, update);
            if (result.MatchedCount == 0)
                throw new Exception("Location not Found");
        }

        public async Task<string> CreateBusinessLocation(BusinessLocation location)
        {
            var storageModel = new StorageBusinessLocation(location);
            await _location.InsertOneAsync(_session, storageModel);
            return storageModel.BusinessId.ToString();
        }

        public async Task DeleteBusinessLocation(string locationId, string managerId)
        {
            var filterBuilder = Builders<StorageBusinessLocation>.Filter;
            var filter = filterBuilder.Empty;
            filter &= filterBuilder.Eq("_id", ObjectId.Parse(locationId));
            filter &= filterBuilder.Eq("ManagerId", managerId);
            var res = await _location.DeleteOneAsync(_session, filter);
            if (res.DeletedCount == 0)
                throw new Exception("Delete Business");
        }

        public async Task RemovePersonFromList(string locationId, string personId)
        {
            var filterBuilder = Builders<StorageBusinessLocation>.Filter;
            var filter = filterBuilder.Empty;
            filter &= filterBuilder.Eq("_id", ObjectId.Parse(locationId));
            var update = Builders<StorageBusinessLocation>.Update
                .Pull(nameof(StorageBusinessLocation.PeopleInLine), personId);
            var result = await _location.UpdateOneAsync(_session, filter, update);
            if (result.MatchedCount == 0)
                throw new Exception("Location not Found");
        }

        public async Task UpdateBusinessLocation(BusinessLocation location)
        {
            var storageModel = new StorageBusinessLocation(location);
            var filterBuilder = Builders<StorageBusinessLocation>.Filter;
            var filter = filterBuilder.Empty;
            filter &= filterBuilder.Eq("_id", storageModel.BusinessId);
            filter &= filterBuilder.Eq("ManagerId", storageModel.ManagerId);
            var update = Builders<StorageBusinessLocation>.Update
                .Set(nameof(StorageBusinessLocation.Name), storageModel.Name)
                .Set(nameof(StorageBusinessLocation.WorkingShifts), storageModel.WorkingShifts)
                .Set(nameof(StorageBusinessLocation.AverageCustomerTimeMinutes), storageModel.AverageCustomerTimeMinutes)
                .Set(nameof(StorageBusinessLocation.BusinessTags), storageModel.BusinessTags)
                .Set(nameof(StorageBusinessLocation.Description), storageModel.Description)
                .Set(nameof(StorageBusinessLocation.WorkingDays), storageModel.WorkingDays)
                .Set(nameof(StorageBusinessLocation.Location), storageModel.Location)
                .Set(nameof(StorageBusinessLocation.OffDays), storageModel.OffDays)
                .Set(nameof(StorageBusinessLocation.SearchText), storageModel.SearchText);
            var res = await _location.UpdateOneAsync(_session, filter, update);
            if (res.MatchedCount == 0)
                throw new Exception("WHAT");
        }
    }
}
