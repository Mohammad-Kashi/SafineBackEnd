using FluentValidation;

namespace SafineBackEnd.Application.Commands.RemovePersonFromList
{
    public class RemovePersonFromListValidation : AbstractValidator<RemovePersonFromListCommand>
    {
        public RemovePersonFromListValidation()
        {
            RuleFor(t => t.LocationId).NotEmpty();
            RuleFor(t => t.ManagerId).NotEmpty();
        }
    }
}
