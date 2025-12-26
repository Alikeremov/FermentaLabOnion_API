using FermentaLabOnion.Application.DTOs.ShareSpecialDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.ShareSpecialValidators
{
    public class ShareSpecialCreateDtoValidator : AbstractValidator<ShareSpecialCreateDto>
    {
        public ShareSpecialCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.Subtitle)
                .NotEmpty()
                .MaximumLength(4000);
            RuleFor(x => x.Image)
                .NotEmpty();
        }
    }
}