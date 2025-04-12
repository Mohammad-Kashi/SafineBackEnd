using Domain.Location.ValueObjects;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Http.Headers;
using SafineBackEnd.Application.Shared;
using SafineGRPC;

namespace SafineBackEnd.Application.Commands.EditLocation
{
    public record EditLocationCommand(string LocationId, string Name, IEnumerable<TimeRange> WorkingShifts, int AverageCustomerTimeMinutes, IEnumerable<string> BusinessTags, string Description, IEnumerable<int> WorkingDays, string Location, IEnumerable<Day> OffDays, string ManagerId) : IRequest<BoolIdArg>, IBaseRequest
    {
        public EditLocationCommand(LocationMessage item, Metadata requestHeader) : this(item.Id, item.Name, item.WorkingShifts.Select(t => new TimeRange(new Time(t.Start.Hour, t.Start.Minute), new Time(t.End.Hour, t.End.Minute))), item.AverageCustomerTimeMinutes, item.BusinessTags, item.Description, item.WorkingDays.Select(t => (int)t), item.Location, item.OffDays.Select(t => new Day(t.Month, t.DayOfMonth)), requestHeader.GetManagerId())
        {
            
        }
    }
}
