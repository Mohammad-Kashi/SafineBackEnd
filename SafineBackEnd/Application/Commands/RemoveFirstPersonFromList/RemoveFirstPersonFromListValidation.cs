using FluentValidation;

namespace SafineBackEnd.Application.Commands.RemoveFirstPersonFromList
{
    public class RemoveFirstPersonFromListValidation : AbstractValidator<RemoveFirstPersonFromListCommand>
    {
        public RemoveFirstPersonFromListValidation()
        {
            RuleFor(t => t.LocationId).NotEmpty();
            RuleFor(t => t.ManagerId).NotEmpty();
        }
    }
}
