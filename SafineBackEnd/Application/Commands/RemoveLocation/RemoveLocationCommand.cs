using Grpc.Core;
using MediatR;
using SafineBackEnd.Application.Shared;
using SafineGRPC;

namespace SafineBackEnd.Application.Commands.RemoveLocation
{
    public record RemoveLocationCommand(string LocationId, string ManagerId) : IRequest<BoolIdArg>, IBaseRequest
    {
        public RemoveLocationCommand(StringIdArg item, Metadata requestHeader) : this(item.Id, requestHeader.GetManagerId())
        {
            
        }
    }
}
