using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Http.Headers;
using SafineBackEnd.Application.Commands.AddLocation;
using SafineBackEnd.Application.Commands.AddPersonToList;
using SafineBackEnd.Application.Commands.EditLocation;
using SafineBackEnd.Application.Commands.RemoveFirstPersonFromList;
using SafineBackEnd.Application.Commands.RemoveLocation;
using SafineBackEnd.Application.Commands.RemovePersonFromList;
using SafineGRPC;

namespace SafineBackEnd.Services
{
    public class CommandService : SafineCommandService.SafineCommandServiceBase
    {
        private readonly IMediator _mediator;
        public CommandService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<StringIdArg> AddLocation(LocationMessage item, ServerCallContext context)
        => await _mediator.Send(new AddLocationCommand(item, context.RequestHeaders));
        public override async Task<BoolIdArg> EditLocation(LocationMessage item, ServerCallContext context)
        => await _mediator.Send(new EditLocationCommand(item, context.RequestHeaders));
        public override async Task<BoolIdArg> RemoveLocation(StringIdArg item, ServerCallContext context)
        => await _mediator.Send(new RemoveLocationCommand(item, context.RequestHeaders));
        public override async Task<BoolIdArg> AddPersonToList(StringIdArg item, ServerCallContext context)
        => await _mediator.Send(new AddPersonToListCommand(item, context.RequestHeaders));
        public override async Task<BoolIdArg> RemovePersonFromList(StringIdArg item, ServerCallContext context)
        => await _mediator.Send(new RemovePersonFromListCommand(item, context.RequestHeaders));
        public override async Task<StringIdArg> RemoveFirstPersonFromList(StringIdArg item, ServerCallContext context)
        => await _mediator.Send(new RemoveFirstPersonFromListCommand(item, context.RequestHeaders));
    }
}
