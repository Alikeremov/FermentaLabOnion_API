using FermentaLabOnion.Application.DTOs.AppDTOs;
using FermentaLabOnion.Application.DTOs.AppTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.AppTranslateValidators
{
    public class AppTranslateCreateDtoValidator : AbstractValidator<AppTranslateCreateDto>
    {
        public AppTranslateCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(4000);

        }
    }
}
