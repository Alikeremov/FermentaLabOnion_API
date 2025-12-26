using FermentaLabOnion.Application.DTOs.ChineseWisdomDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.ChineseWisdomValidators
{
    public class ChineseWisdomUpdateDtoValidator : AbstractValidator<ChineseWisdomUpdateDto>
    {
        public ChineseWisdomUpdateDtoValidator()
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
