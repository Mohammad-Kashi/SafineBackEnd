using Domain.IRepository;
using Domain.Location;
using MediatR;
using SafineGRPC;

namespace SafineBackEnd.Application.Commands.EditLocation
{
    public class EditLocationCommandHandler : IRequestHandler<EditLocationCommand, BoolIdArg>
    {
        private readonly ICommandRepository _commandRepo;
        private readonly ITransaction _transaction;
        public EditLocationCommandHandler(ICommandRepository command, ITransaction transaction)
        {
            _commandRepo = command;
            _transaction = transaction;
        }
        public async Task<BoolIdArg> Handle(EditLocationCommand request, CancellationToken cancellationToken)
        {
            await _commandRepo.UpdateBusinessLocation(new BusinessLocation(request.LocationId, request.ManagerId, request.Name, request.WorkingShifts.ToList(), request.AverageCustomerTimeMinutes, request.BusinessTags.ToList(), request.Description, request.WorkingDays.ToList(), request.Location, request.OffDays.ToList(), new List<string>()));
            await _transaction.CommitAsync();
            return new BoolIdArg
            {
                Id = true
            };
        }
    }
}
