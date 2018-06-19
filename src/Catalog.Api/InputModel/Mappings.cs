using System;
using AutoMapper;
using Catalog.Api.DomainModel;

namespace Catalog.Api.InputModel
{
    public static class Mappings
    {
        public static void ConfigureInputModelMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Product, UpdateProductInputModel>()
                .ForSourceMember(x => x.LastUpdated, memberCfg => memberCfg.Ignore());

            cfg.CreateMap<UpdateProductInputModel, Product>()
                .ForMember(x => x.Id, memberCfg => memberCfg.Ignore())
                .ForMember(x => x.LastUpdated, memberCfg => memberCfg.UseValue(DateTime.UtcNow));

            cfg.CreateMap<Product, AddProductInputModel>()
                .ForSourceMember(x => x.LastUpdated, memberCfg => memberCfg.Ignore())
                .ForSourceMember(x => x.Id, memberCgf => memberCgf.Ignore());

            cfg.CreateMap<AddProductInputModel, Product>()
                .ForMember(x => x.LastUpdated, memberCfg => memberCfg.UseValue(DateTime.UtcNow))
                .ForMember(x => x.Id, memberCfg => memberCfg.Ignore());
        }
    }
}