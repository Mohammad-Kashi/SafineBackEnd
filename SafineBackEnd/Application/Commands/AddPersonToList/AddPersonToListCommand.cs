using Grpc.Core;
using MediatR;
using SafineBackEnd.Application.Shared;
using SafineGRPC;

namespace SafineBackEnd.Application.Commands.AddPersonToList
{
    public record AddPersonToListCommand(string LocationId, string ManagerId) : IRequest<BoolIdArg>, IBaseRequest
    {
        public AddPersonToListCommand(StringIdArg item, Metadata requestHeader) : this(item.Id, requestHeader.GetManagerId())
        {
            
        }
    }
}
