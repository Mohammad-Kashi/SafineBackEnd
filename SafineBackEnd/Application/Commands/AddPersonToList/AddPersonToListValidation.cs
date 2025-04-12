using FluentValidation;

namespace SafineBackEnd.Application.Commands.AddPersonToList
{
    public class AddPersonToListValidation : AbstractValidator<AddPersonToListCommand>
    {
        public AddPersonToListValidation()
        {
            RuleFor(t => t.ManagerId).NotEmpty();
            RuleFor(t => t.LocationId).NotEmpty();
        }
    }
}
