using Domain.IRepository;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Commands.AddPersonToList
{
    public class AddPersonToListCommandHandler : IRequestHandler<AddPersonToListCommand, BoolIdArg>
    {
        private readonly ICommandRepository _commandRepo;
        private readonly ITransaction _transaction;
        public AddPersonToListCommandHandler(ICommandRepository command, ITransaction transaction)
        {
            _commandRepo = command;
            _transaction = transaction;
        }
        public async Task<BoolIdArg> Handle(AddPersonToListCommand request, CancellationToken cancellationToken)
        {
            await _commandRepo.AddPersonToList(request.LocationId, request.ManagerId);
            await _transaction.CommitAsync();
            return new BoolIdArg
            {
                Id = true
            };
        }
    }
}
