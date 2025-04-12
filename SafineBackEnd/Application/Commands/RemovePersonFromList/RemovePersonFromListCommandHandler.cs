using Domain.IRepository;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Commands.RemovePersonFromList
{
    public class RemovePersonFromListCommandHandler : IRequestHandler<RemovePersonFromListCommand, BoolIdArg>
    {
        private readonly ICommandRepository _commandRepo;
        private readonly ITransaction _transaction;
        public RemovePersonFromListCommandHandler(ICommandRepository command, ITransaction transaction)
        {
            _commandRepo = command;
            _transaction = transaction;
        }
        public async Task<BoolIdArg> Handle(RemovePersonFromListCommand request, CancellationToken cancellationToken)
        {
            await _commandRepo.RemovePersonFromList(request.LocationId, request.ManagerId);
            await _transaction.CommitAsync();
            return new BoolIdArg
            {
                Id = true
            };
        }
    }
}
