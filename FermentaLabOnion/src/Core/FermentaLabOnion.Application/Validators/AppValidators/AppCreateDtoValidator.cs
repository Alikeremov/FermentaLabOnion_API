using FermentaLabOnion.Application.DTOs.AppDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.AppValidators
{
    public class AppCreateDtoValidator : AbstractValidator<AppCreateDto>
    {
        public AppCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(4000);
            RuleFor(x=>x.Image)
                .NotEmpty();
        }
    }
}
