using FermentaLabOnion.Application.DTOs.TagTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.TagTranslateValidator
{
    public class TagTranslateUpdateDtoValidator : AbstractValidator<TagTranslateUpdateDto>
    {
        public TagTranslateUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("You can not update category without name")
                .MaximumLength(200).WithMessage("You can not use caracter above 200 for update category");
        }
    }
}
