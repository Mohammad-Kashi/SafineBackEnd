using Domain.IRepository;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetLocationInfo
{
    public class GetLocationInfoQueryHandler : IRequestHandler<GetLocationInfoQuery, LocationMessage>
    {
        private readonly IQueryRepository _queryRepo;
        public GetLocationInfoQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepo = queryRepository;
        }
        public async Task<LocationMessage> Handle(GetLocationInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _queryRepo.GetBusinessLocationById(request.LocationId);
            if (result is null)
                throw new Exception("Business Location Not Found");
            var cmd = new LocationMessage
            {
                Id = result.Id,
                Description = result.Description,
                AverageCustomerTimeMinutes = result.AverageCustomerTimeMinutes,
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
        }
    }
}
