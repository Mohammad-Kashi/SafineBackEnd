using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Http.Headers;
using SafineBackEnd.Application.Queries.GetLocationInfo;
using SafineBackEnd.Application.Queries.GetLocationsBySearchArg;
using SafineBackEnd.Application.Queries.GetManagerLocations;
using SafineBackEnd.Application.Queries.GetRemainingTimeInLine;
using SafineBackEnd.Application.Queries.GetTags;
using SafineGRPC;

namespace SafineBackEnd.Services
{
    public class QueryService : SafineQueryService.SafineQueryServiceBase
    {
        private readonly IMediator _mediator;
        public QueryService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async override Task<LocationMessage> GetLocationInfo(StringIdArg item, ServerCallContext context)
        => await _mediator.Send(new GetLocationInfoQuery(item, context.RequestHeaders));
        public override async Task<LocationMessages> GetManagerLocations(Empty item, ServerCallContext context)
        => await _mediator.Send(new GetManagerLocationsQuery(item, context.RequestHeaders));
        public override async Task<LocationMessages> GetLocationsBySearchArg(SearchArg item, ServerCallContext context)
        => await _mediator.Send(new GetLocationsBySearchArgQuery(item, context.RequestHeaders));
        public override async Task<IntIdArg> GetRemainingTimeInLine(StringIdArg item, ServerCallContext context)
        => await _mediator.Send(new GetRemainingTimeInLineQuery(item, context.RequestHeaders));
        public override async Task<GetTagsResponseProto> GetTags(Empty item, ServerCallContext context)
        => await _mediator.Send(new GetTagsQuery());
    }
}
