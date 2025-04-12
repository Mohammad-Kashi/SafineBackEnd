using Domain.IRepository;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetRemainingTimeInLine
{
    public class GetRemainingTimeInLineQueryHandler : IRequestHandler<GetRemainingTimeInLineQuery, IntIdArg>
    {
        private readonly IQueryRepository _queryRepo;
        public GetRemainingTimeInLineQueryHandler(IQueryRepository queryRepository)
        {
            _queryRepo = queryRepository;
        }
        public async Task<IntIdArg> Handle(GetRemainingTimeInLineQuery request, CancellationToken cancellationToken)
        {
            var result = await _queryRepo.TimeRemaining(request.LocationId, request.ManagerId);
            return new IntIdArg
            {
                Id = result
            };
        }
    }
}
