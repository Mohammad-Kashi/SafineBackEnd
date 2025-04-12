using Domain.Antipattern;
using Domain.IRepository;
using Domain.Location;
using Domain.Location.ValueObjects;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetLocationsBySearchArg
{
    public class GetLocationsBySearchArgQueryHandler : IRequestHandler<GetLocationsBySearchArgQuery, LocationMessages>
    {
        private readonly IQueryRepository _queryRepo;
        public GetLocationsBySearchArgQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepo = queryRepository;
        }
        public async Task<LocationMessages> Handle(GetLocationsBySearchArgQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _queryRepo.GetAllBusinessLocationsBySearch(new BusinessSearchArg(request.SearchText, request.BusinessTags));
            var final = new LocationMessages
            {

            };
            
            var results = request.IsOpen ? queryResult.Where(t => IsOpenLocation(t)) : queryResult;
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
                return cmd;
            }));
            return final;
        }
        private bool IsOpenLocation(BusinessLocation location)
        {
            var now = new DateTimeOffset(DateTime.UtcNow, new TimeSpan(3, 30, 0));
            if (location.OffDays.Any(t => t.Month == now.Month && t.DayOfMonth == now.Day))
                return false;
            var dayOfWeek = (((int)now.DayOfWeek) + 1) % 7;
            if (!location.WorkingDays.Any(t => t == dayOfWeek))
                return false;

            var nowTime = new Time(now.Hour, now.Minute);
            foreach (var time in location.WorkingShifts)
            {
                if (time.IsInTimeRange(nowTime))
                    return true;
            }
            return false;
        }
    }
}
