using AutoMapper;
using Catalog.Api.DomainModel;

namespace Catalog.Api.InputModel
{
    public static class Mappings
    {
        public static void ConfigureInputModelMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Product, UpdateProductInputModel>()
                .ForMember(x => x.ConfirmPrice, memberCfg => memberCfg.Ignore())
                .ForSourceMember(x => x.LastUpdated, memberCfg => memberCfg.Ignore());

            cfg.CreateMap<UpdateProductInputModel, Product>()
                .ForMember(x => x.Id, memberCfg => memberCfg.Ignore())
                .ForMember(x => x.LastUpdated, memberCfg => memberCfg.Ignore())
                .ForSourceMember(x => x.ConfirmPrice, memberCfg => memberCfg.Ignore());

            cfg.CreateMap<Product, AddProductInputModel>()
                .ForMember(x => x.ConfirmPrice, memberCfg => memberCfg.Ignore())
                .ForSourceMember(x => x.LastUpdated, memberCfg => memberCfg.Ignore())
                .ForSourceMember(x => x.Id, memberCgf => memberCgf.Ignore());

            cfg.CreateMap<AddProductInputModel, Product>()
                .ForMember(x => x.LastUpdated, memberCfg => memberCfg.Ignore())
                .ForMember(x => x.Id, memberCfg => memberCfg.Ignore())
                .ForSourceMember(x => x.ConfirmPrice, memberCfg => memberCfg.Ignore());
        }
    }
}