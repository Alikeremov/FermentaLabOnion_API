using FermentaLabOnion.Application.DTOs.QuestionTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.QuestionTranslateValidators
{
    public class QuestionTranslateCreateDtoValidator : AbstractValidator<QuestionTranslateCreateDto>
    {
        public QuestionTranslateCreateDtoValidator()
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
