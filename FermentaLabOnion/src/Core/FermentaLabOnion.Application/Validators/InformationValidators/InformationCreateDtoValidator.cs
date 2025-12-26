using FermentaLabOnion.Application.DTOs.InformationDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.InformationValidators
{
    public class InformationCreateDtoValidator : AbstractValidator<InformationCreateDto>
    {
        public InformationCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.Image)
                .NotEmpty();
        }
    }
}
