using Domain.IRepository;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetManagerLocations
{
    public class GetManagerLocationsQueryHandler : IRequestHandler<GetManagerLocationsQuery, LocationMessages>
    {
        private readonly IQueryRepository _queryRepo;
        public GetManagerLocationsQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepo = queryRepository;
        }
        public async Task<LocationMessages> Handle(GetManagerLocationsQuery request, CancellationToken cancellationToken)
        {
            var results = await _queryRepo.GetBusinessLocationsByManagerId(request.ManagerId);
            var final = new LocationMessages
            {

            };
            final.Locations.AddRange(results.Select(result =>
            {
                var cmd = new LocationMessage
                {
                    Id = result.Id,
                    Description = result.Description,
                    Location = result.Location,
                    Name = result.Name,
                };
                cmd.WorkingShifts.AddRange(result.WorkingShifts.Select(t => new TimeRangeMessage
                {
                    Start = new TimeMessage
                    {
                        Hour = t.StartTime.Hour,
                        Minute = t.StartTime.Minute,
                    },
                    End = new TimeMessage
                    {
                        Hour = t.EndTime.Hour,
                        Minute = t.EndTime.Minute,
                    }
                }));
                cmd.BusinessTags.AddRange(result.BusinessTags);
                cmd.WorkingDays.AddRange(result.WorkingDays.Select(t => (Days)t));
                cmd.OffDays.AddRange(result.OffDays.Select(t => new DayMessage
                {
                    Month = t.Month,
                    DayOfMonth = t.DayOfMonth,
                }));
                cmd.PeopleInLine.AddRange(result.PeopleInLine);
                return cmd;
            }));
            return final;
        }
    }
}
