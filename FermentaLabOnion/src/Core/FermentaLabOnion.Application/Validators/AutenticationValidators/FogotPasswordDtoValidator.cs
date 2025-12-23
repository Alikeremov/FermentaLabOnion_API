using FermentaLabOnion.Application.DTOs.AutenticationDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.AutenticationValidators
{
    public class FogotPasswordDtoValidator:AbstractValidator<FogotPasswordDto>
    {
        public FogotPasswordDtoValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Email formatı yanlışdır");
        }
    }
}
