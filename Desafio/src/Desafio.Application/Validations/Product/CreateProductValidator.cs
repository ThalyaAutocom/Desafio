using FluentValidation;

namespace Desafio.Application.Validations.Product;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductService _productService;

    public CreateProductValidator(IProductService productService)
    {
        _productService = productService;

        RuleFor(x => x.BarCode)
            .MustAsync(async(barCode, _) => !await _productService.ExistingBarCodeAsync(barCode)).WithMessage("The barcode is already been used.")
            .Length(1, 13).WithMessage("BarCode must have between {MinLength} and {MaxLength} caracteres.").Unless(x => string.IsNullOrWhiteSpace(x.BarCode));
        RuleFor(x => x.Acronym)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .MustAsync(async(acronym, _) => await _productService.UnitAlreadyExistsAsync(acronym)).WithMessage("The unit is not registered. It is necessary to register before using in product registration.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.ShortDescription)
            .NotEmpty().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0m).WithMessage("The price cannot be negative.");
    }
}
