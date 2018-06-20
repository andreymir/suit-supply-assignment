using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Api.DomainModel;
using Catalog.Api.Infrastructure;
using Catalog.Api.InputModels;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Validators
{
    public class UpdateProductModelValidator : AbstractValidator<UpdateProductInputModel>
    {
        private readonly CatalogContext _catalogContext;

        public UpdateProductModelValidator(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;

            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Code).NotEmpty().MustAsync(CheckCodeUnique);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.ConfirmPrice).Must(CheckPrice);
        }

        private async Task<bool> CheckCodeUnique(UpdateProductInputModel model, string value, PropertyValidatorContext context, CancellationToken cancellationToken)
        {
            return await _catalogContext.Products
                .AllAsync(
                    x => x.Id == model.Id || x.Code != value,
                    cancellationToken);
        }

        private static bool CheckPrice(UpdateProductInputModel model, bool value)
        {
            return new PriceValidator(model.Price, value).Validate();
        }
    }
}
