using Grpc.Core;
using MediatR;
using SafineBackEnd.Application.Shared;
using SafineGRPC;

namespace SafineBackEnd.Application.Commands.RemovePersonFromList
{
    public record RemovePersonFromListCommand(string LocationId, string ManagerId) : IRequest<BoolIdArg>, IBaseRequest
    {
        public RemovePersonFromListCommand(StringIdArg item, Metadata requestHeader) : this(item.Id, requestHeader.GetManagerId())
        {
            
        }
    }
}
