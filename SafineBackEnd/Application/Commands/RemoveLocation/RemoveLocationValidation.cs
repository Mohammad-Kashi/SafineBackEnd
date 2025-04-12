using FluentValidation;

namespace SafineBackEnd.Application.Commands.RemoveLocation
{
    public class RemoveLocationValidation : AbstractValidator<RemoveLocationCommand>
    {
        public RemoveLocationValidation()
        {
            RuleFor(t => t.LocationId).NotEmpty();
            RuleFor(t => t.ManagerId).NotEmpty();
        }
    }
}
