using Domain.IRepository;
using Domain.Location;
using MediatR;
using SafineGRPC;
using System.Runtime.CompilerServices;

namespace SafineBackEnd.Application.Commands.AddLocation
{
    public class AddLocationCommandHandler : IRequestHandler<AddLocationCommand, StringIdArg>
    {
        private readonly ICommandRepository _commandRepo;
        private readonly ITransaction _transaction;
        public AddLocationCommandHandler(ICommandRepository command, ITransaction transaction)
        {
            _commandRepo = command;
            _transaction = transaction;
        }
        public async Task<StringIdArg> Handle(AddLocationCommand request, CancellationToken cancellationToken)
        {
            var locationId = await _commandRepo.CreateBusinessLocation(new BusinessLocation(null, request.ManagerId, request.Name, request.WorkingShifts.ToList(), request.AverageCustomerTimeMinutes, request.BusinessTags.ToList(), request.Description, request.WorkingDays.ToList(), request.Location, request.OffDays.ToList(), new List<string>()));
            await _transaction.CommitAsync(cancellationToken);
            return new StringIdArg
            {
                Id = locationId
            };
        }
    }
}
