using Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.NewPropertyValidator.BaseValidators
{
    public class NewPropertyValidator : AbstractValidator<NewProperty>
    {

        public NewPropertyValidator()
        {
            RuleFor(np => np.Name)
                .NotEmpty().WithMessage("name is required")
                .MaximumLength(10)
                .WithMessage("name should not accept more than 10 chars");
        }
    }
}
