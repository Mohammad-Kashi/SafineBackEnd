using Domain.Antipattern;
using Domain.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IQueryRepository
    {
        Task<BusinessLocation> GetBusinessLocationById(string locationId);
        Task<IEnumerable<BusinessLocation>> GetBusinessLocationsByManagerId(string managerId);
        Task<IEnumerable<BusinessLocation>> GetAllBusinessLocationsBySearch(BusinessSearchArg searchArg);
        Task<int> TimeRemaining(string locationId, string managerId);
        Task<string> GetFirstInLine(string locationId, string managerId);
        Task<List<string>> GetTags();
    }
}
