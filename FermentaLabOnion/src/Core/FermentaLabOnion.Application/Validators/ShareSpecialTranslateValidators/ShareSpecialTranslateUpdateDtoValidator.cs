using FermentaLabOnion.Application.DTOs.ShareSpecialTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.ShareSpecialTranslateValidators
{
    public class ShareSpecialTranslateUpdateDtoValidator : AbstractValidator<ShareSpecialTranslateUpdateDto>
    {
        public ShareSpecialTranslateUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.Subtitle)
                .NotEmpty()
                .MaximumLength(4000);
        }
    }
}
