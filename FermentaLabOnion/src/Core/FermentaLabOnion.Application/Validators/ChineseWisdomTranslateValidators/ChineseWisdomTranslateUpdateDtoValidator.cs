using FermentaLabOnion.Application.DTOs.ChineseWisdomTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.ChineseWisdomTranslateValidators
{
    public class ChineseWisdomTranslateUpdateDtoValidator : AbstractValidator<ChineseWisdomTranslateUpdateDto>
    {
        public ChineseWisdomTranslateUpdateDtoValidator()
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
