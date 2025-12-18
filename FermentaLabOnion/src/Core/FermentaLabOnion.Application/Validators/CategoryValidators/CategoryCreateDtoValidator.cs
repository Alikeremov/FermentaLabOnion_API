using FermentaLabOnion.Application.DTOs.CategoryDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.CategoryValidators
{
    public class CategoryCreateDtoValidator:AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("You can not create category without name")
                .MaximumLength(202).WithMessage("You can not use caracter above 202 for create category");
        }
    }
}
