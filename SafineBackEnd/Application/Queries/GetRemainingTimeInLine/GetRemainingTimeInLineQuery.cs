using Grpc.Core;
using MediatR;
using SafineBackEnd.Application.Shared;
using SafineGRPC;

namespace SafineBackEnd.Application.Queries.GetRemainingTimeInLine
{
    public record GetRemainingTimeInLineQuery(string LocationId, string ManagerId) : IRequest<IntIdArg>, IBaseRequest
    {
        public GetRemainingTimeInLineQuery(StringIdArg item, Metadata requestHeader) : this(item.Id, requestHeader.GetManagerId())
        {
            
        }
    }
}
