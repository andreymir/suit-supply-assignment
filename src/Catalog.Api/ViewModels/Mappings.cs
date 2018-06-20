using AutoMapper;
using Catalog.Api.DomainModel;

namespace Catalog.Api.ViewModels
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