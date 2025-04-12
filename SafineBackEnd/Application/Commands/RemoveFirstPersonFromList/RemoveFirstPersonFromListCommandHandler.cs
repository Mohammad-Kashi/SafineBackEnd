using Domain.IRepository;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Commands.RemoveFirstPersonFromList
{
    public class RemoveFirstPersonFromListCommandHandler : IRequestHandler<RemoveFirstPersonFromListCommand, StringIdArg>
    {
        private readonly ICommandRepository _commandRepo;
        private readonly IQueryRepository _queryRepo;
        private readonly ITransaction _transaction;
        public RemoveFirstPersonFromListCommandHandler(ICommandRepository command, IQueryRepository queryRepository, ITransaction transaction)
        {
            _commandRepo = command;
            _transaction = transaction;
            _queryRepo = queryRepository;
        }
        public async Task<StringIdArg> Handle(RemoveFirstPersonFromListCommand request, CancellationToken cancellationToken)
        {
            var firstPerson = await _queryRepo.GetFirstInLine(request.LocationId, request.ManagerId);
            if (string.IsNullOrEmpty(firstPerson))
                return new StringIdArg
                {
                    Id = string.Empty
                };
            await _commandRepo.RemovePersonFromList(request.LocationId, firstPerson);
            await _transaction.CommitAsync();
            return new StringIdArg
            {
                Id = firstPerson
            };
        }
    }
}
