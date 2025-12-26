using FermentaLabOnion.Application.DTOs.InformationTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.InformationTranslateValidators
{
    public class InformationTranslateUpdateDtoValidator : AbstractValidator<InformationTranslateUpdateDto>
    {
        public InformationTranslateUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
