using FermentaLabOnion.Application.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FermentaLabOnion.Application.Validators.ProductValidators
{
    internal class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
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

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.OldPrice)
                .GreaterThan(0)
                .When(x => x.OldPrice.HasValue);

            RuleFor(x => x.SKU)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0);

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

            RuleFor(x => x.CategoryId)
                .NotNull()
                .WithMessage("Category must chosen");
        }
    }
}
