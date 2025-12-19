using FermentaLabOnion.Application.DTOs.TagTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.TagTranslateValidator
{
    internal class TagTranslateCreateDtoValidator : AbstractValidator<TagTranslateCreateDto>
    {
        public TagTranslateCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("You can not create TagTranslate without name")
                .MaximumLength(200).WithMessage("You can not use caracter above 200 for create TagTranslate");
        }
    }
}
