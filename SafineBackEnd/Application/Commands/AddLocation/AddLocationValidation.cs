using FluentValidation;

namespace SafineBackEnd.Application.Commands.AddLocation
{
    public class AddLocationValidation : AbstractValidator<AddLocationCommand>
    {
        public AddLocationValidation()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.WorkingShifts).NotEmpty();
            RuleFor(t => t.AverageCustomerTimeMinutes).NotEmpty();
            RuleFor(t => t.WorkingDays).NotEmpty();
            RuleFor(t => t.ManagerId).NotEmpty();
        }
    }
}
