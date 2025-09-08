using FluentValidation;
using FluentValidation.Validators;
using SmartTeach.App.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Validators
{
    public class AddGroupDtoValidator : AbstractValidator<AddGroupDto>
    {
        public AddGroupDtoValidator()
        {
            RuleFor(a => a.Name)
                  .NotEmpty().WithMessage("name is required.");
            RuleFor(a => a.Subject).NotEmpty().WithMessage("Subject is required.");
            RuleFor(a => a.centerd)
                .NotEmpty().WithMessage("centerd is required.")
                .Length(3, 50).WithMessage("centerd must be between 3 and 50 characters.");

        }
    }
}
