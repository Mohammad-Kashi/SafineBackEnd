using Grpc.Core;
using MediatR;
using SafineBackEnd.Application.Shared;
using SafineGRPC;

namespace SafineBackEnd.Application.Commands.RemoveFirstPersonFromList
{
    public record RemoveFirstPersonFromListCommand(string LocationId, string ManagerId) : IRequest<StringIdArg>, IBaseRequest
    {
        public RemoveFirstPersonFromListCommand(StringIdArg item, Metadata requestHeader) : this(item.Id, requestHeader.GetManagerId()) { }
    }
}
