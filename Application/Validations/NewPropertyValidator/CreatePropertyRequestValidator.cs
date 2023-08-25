using Application.Features.Properties.Commands;
using FluentValidation;

namespace Application.Validations.NewPropertyValidator
{
    public class CreatePropertyRequestValidator : AbstractValidator<CreatePropertyRequest>
    {
        public CreatePropertyRequestValidator()
        {
            RuleFor(x => x.PropertyRequest)
                .SetValidator(new Validations.NewPropertyValidator.BaseValidators.NewPropertyValidator());
        }
    }
}
