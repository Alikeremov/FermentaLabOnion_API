using FermentaLabOnion.Application.DTOs.ChineseWisdomDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.ChineseWisdomValidators
{
    public class ChineseWisdomCreateDtoValidator : AbstractValidator<ChineseWisdomCreateDto>
    {
        public ChineseWisdomCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(4000);
            RuleFor(x => x.Image)
                .NotEmpty();
        }
    }
}
