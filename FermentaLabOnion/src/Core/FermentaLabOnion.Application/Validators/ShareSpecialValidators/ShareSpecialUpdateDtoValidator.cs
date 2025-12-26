using FermentaLabOnion.Application.DTOs.ShareSpecialDTOs;
using FermentaLabOnion.Application.DTOs.ShareSpecialTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.ShareSpecialValidators
{
    public class ShareSpecialUpdateDtoValidator : AbstractValidator<ShareSpecialUpdateDto>
    {
        public ShareSpecialUpdateDtoValidator()
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
