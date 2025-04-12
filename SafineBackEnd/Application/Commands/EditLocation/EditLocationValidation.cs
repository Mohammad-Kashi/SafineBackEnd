using FluentValidation;

namespace SafineBackEnd.Application.Commands.EditLocation
{
    public class EditLocationValidation : AbstractValidator<EditLocationCommand>
    {
        public EditLocationValidation()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.WorkingShifts).NotEmpty();
            RuleFor(t => t.AverageCustomerTimeMinutes).NotEmpty();
            RuleFor(t => t.WorkingDays).NotEmpty();
            RuleFor(t => t.ManagerId).NotEmpty();
            RuleFor(t => t.LocationId).NotEmpty();
        }
    }
}
