using FermentaLabOnion.Application.DTOs.HeritageProcessTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.HeritageProcessTranslateValidators
{
    public class HeritageProcessTranslateCreateDtoValidator : AbstractValidator<HeritageProcessTranslateCreateDto>
    {
        public HeritageProcessTranslateCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.BeforeLabel)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.AfterLabel)
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
