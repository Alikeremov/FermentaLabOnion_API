using FermentaLabOnion.Application.DTOs.TagDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.TagValidators
{
    public class TagCreateDtoValidator:AbstractValidator<TagCreateDto>
    {
        public TagCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("You can not create Tag without name")
                .MaximumLength(200).WithMessage("You can not use caracter above 200 for create Tag");
        }
    }
}
