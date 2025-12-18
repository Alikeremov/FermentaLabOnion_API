using FermentaLabOnion.Application.DTOs.CategoryTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.CategoryTranslateValidators
{
    public class CategoryTranslateUpdateDtoValidator:AbstractValidator<CategoryTranslateUpdateDto>
    {
        public CategoryTranslateUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("You can not create category translate without name")
                .MaximumLength(202).WithMessage("You can not use caracter above 202 for create category translate");
        }
    }
}
