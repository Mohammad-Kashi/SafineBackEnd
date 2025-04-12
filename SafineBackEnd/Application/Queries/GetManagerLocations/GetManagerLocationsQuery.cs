using Grpc.Core;
using MediatR;
using SafineBackEnd.Application.Shared;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetManagerLocations
{
    public record GetManagerLocationsQuery(string ManagerId) : IRequest<LocationMessages>, IBaseRequest
    {
        public GetManagerLocationsQuery(Empty item, Metadata requestHeader) : this(requestHeader.GetManagerId())
        {
            
        }
    }
}
