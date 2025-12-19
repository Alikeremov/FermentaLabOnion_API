using FermentaLabOnion.Application.DTOs.ProductTranslateDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.ProductTranslateValidators
{
    public class ProductTranslateUpdateDtoValidator : AbstractValidator<ProductTranslateUpdateDto>
    {
        public ProductTranslateUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.Slug)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.ShortDescription)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(4000);

            RuleFor(x => x.QuantityPerPackage)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Volume)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.PackageType)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Ingredients)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.Benefits)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.UsageInstructions)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.Warnings)
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(x => x.Brand)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.CountryOfOrigin)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.ShelfLife)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.ProductId)
                .NotNull()
                .WithMessage("you must send ProductId");

            RuleFor(x => x.Language)
                .IsInEnum()
                .WithMessage("Language is not chosen truely");
        }
    }
}
