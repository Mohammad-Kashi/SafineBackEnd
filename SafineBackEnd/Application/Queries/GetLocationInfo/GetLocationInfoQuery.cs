using Grpc.Core;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetLocationInfo
{
    public record GetLocationInfoQuery(string LocationId) : IRequest<LocationMessage>, IBaseRequest
    {
        public GetLocationInfoQuery(StringIdArg item, Metadata requestHeader) : this(item.Id)
        {
            
        }
    }
}
