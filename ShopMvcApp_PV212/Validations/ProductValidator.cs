using FluentValidation;
using ShopMvcApp_PV212.Entities;

namespace ShopMvcApp_PV212.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name)
               .NotEmpty()
               .MinimumLength(2)
               .Matches("[A-Z].*").WithMessage("{PropertyName} must starts with uppercase letter.");
            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.Description)
                .MaximumLength(100);
            RuleFor(x => x.ImageUrl).Must(BeAValidUrl).WithMessage("Inage url address must be valid.");
        }

        private bool BeAValidUrl(string? url)
        {
            Uri? uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && uriResult.Scheme == Uri.UriSchemeHttp;
        }
    }
}
