using FermentaLabOnion.Application.DTOs.QuestionDTOs;
using FermentaLabOnion.Application.DTOs.QuestionTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.QuestionValidators
{
    public class QuestionUpdateDtoValidator : AbstractValidator<QuestionUpdateDto>
    {
        public QuestionUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(4000);
        }
    }
}
