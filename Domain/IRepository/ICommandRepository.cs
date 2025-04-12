using Domain.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface ICommandRepository
    {
        Task<string> CreateBusinessLocation(BusinessLocation location);
        Task UpdateBusinessLocation(BusinessLocation location);
        Task DeleteBusinessLocation(string locationId, string managerId);
        Task AddPersonToList(string locationId, string personId);
        Task RemovePersonFromList(string locationId, string personId);
    }
}
