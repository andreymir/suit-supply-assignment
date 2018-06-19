using AutoMapper;
using Catalog.Api.DomainModel;

namespace Catalog.Api.ViewModel
{
    public class Mappings
    {
        public static void ConfigureViewModelMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Product, ProductViewModel>();
            cfg.CreateMap<ProductViewModel, Product>();
        }
    }
}