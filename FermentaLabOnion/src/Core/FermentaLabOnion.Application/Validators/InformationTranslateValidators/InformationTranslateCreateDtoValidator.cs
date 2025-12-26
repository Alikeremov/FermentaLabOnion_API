using FermentaLabOnion.Application.DTOs.AppDTOs;
using FermentaLabOnion.Application.DTOs.InformationDTOs;
using FermentaLabOnion.Application.DTOs.InformationTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.InformationTranslateValidators
{
    public class InformationTranslateCreateDtoValidator : AbstractValidator<InformationTranslateCreateDto>
    {
        public InformationTranslateCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
