using System.Threading;
using System.Threading.Tasks;
using Catalog.Api.DomainModel;
using Catalog.Api.Infrastructure;
using Catalog.Api.InputModel;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Validators
{
    public class AddProductModelValidator : AbstractValidator<AddProductInputModel>
    {
        private readonly CatalogContext _catalogContext;

        public AddProductModelValidator(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Code).NotEmpty().MustAsync(CheckCodeUnique);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.ConfirmPrice).Must(CheckPrice);
        }

        private static bool CheckPrice(AddProductInputModel model, bool value)
        {
            return new PriceValidator(model.Price, value).Validate();
        }

        private async Task<bool> CheckCodeUnique(string value, CancellationToken cancellationToken)
        {
            return await _catalogContext.Products.AllAsync(x => x.Code != value, cancellationToken);
            
        }
    }
}