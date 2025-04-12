using Domain.IRepository;
using MediatR;
using MongoDB.Bson;
using SafineGRPC;
using System.Reflection.Metadata.Ecma335;

namespace SafineBackEnd.Application.Commands.RemoveLocation
{
    public class RemoveLocationCommandHandler : IRequestHandler<RemoveLocationCommand, BoolIdArg>
    {
        private readonly ICommandRepository _commandRepo;
        private readonly ITransaction _transaction;
        public RemoveLocationCommandHandler(ICommandRepository command, ITransaction transaction)
        {
            _commandRepo = command;
            _transaction = transaction;
        }
        public async Task<BoolIdArg> Handle(RemoveLocationCommand request, CancellationToken cancellationToken)
        {
            await _commandRepo.DeleteBusinessLocation(request.LocationId, request.ManagerId);
            await _transaction.CommitAsync();
            return new BoolIdArg
            {
                Id = true
            };
        }
    }
}
