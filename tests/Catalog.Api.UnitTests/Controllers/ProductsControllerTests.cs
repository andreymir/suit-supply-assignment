using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Catalog.Api.Controllers;
using Catalog.Api.DomainModel;
using Catalog.Api.Infrastructure;
using Catalog.Api.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Catalog.Api.UnitTests.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Fixture _fixture;
        private IMapper _mapper;
        private CatalogContext _dbContext;
        private ProductsController _controller;
        private List<Product> _products;

        [SetUp]
        public async Task SetUp()
        {
            _fixture = new Fixture();

            _products = _fixture.CreateMany<Product>(10).ToList();

            var options = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: $"Catalog_{Guid.NewGuid()}")
                .Options;
            _dbContext = new CatalogContext(options);
            await _dbContext.Products.AddRangeAsync(_products);
            await _dbContext.SaveChangesAsync();

            var config = new MapperConfiguration(cfg =>
            {
                Mappings.ConfigureInputModelMapping(cfg);
                Api.ViewModels.Mappings.ConfigureViewModelMapping(cfg);
            });
            _mapper = new Mapper(config);

            _controller = new ProductsController(_dbContext, _mapper);
        }

        [Test]
        public async Task When_no_product_with_id_then_GetById_return_NotFound()
        {
            var product = _fixture.Create<Product>();

            var result = await _controller.GetById(product.Id);

            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task When_have_product_in_db_then_GetById_return_Product()
        {
            var product = _fixture.Create<Product>();
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            var result = await _controller.GetById(product.Id);

            Assert.That(result.Value?.Id, Is.EqualTo(product.Id));
        }

        [Test]
        public async Task When_product_name_starts_with_term_then_Search_return_products()
        {
            var term = _fixture.Create<string>();
            var expectedProducts = new SortedSet<int>();
            for (var i = 0; i < 5; i++)
            {
                var product = _fixture.Create<Product>();
                // prefix product name with term
                product.Name = term + product.Name;
                expectedProducts.Add(product.Id);
                await _dbContext.Products.AddAsync(product);
            }

            await _dbContext.SaveChangesAsync();

            var result = await _controller.Search(term);
            var actualProducts = new SortedSet<int>(result.Value.Select(x => x.Id));
            Assert.That(actualProducts, Is.EquivalentTo(expectedProducts));
        }

        [Test]
        public async Task When_product_code_starts_with_term_then_Search_return_products()
        {
            var term = _fixture.Create<string>();
            var expectedProducts = new SortedSet<int>();
            for (var i = 0; i < 5; i++)
            {
                var product = _fixture.Create<Product>();
                // prefix product name with term
                product.Code = term + product.Code;
                expectedProducts.Add(product.Id);
                await _dbContext.Products.AddAsync(product);
            }

            await _dbContext.SaveChangesAsync();

            var result = await _controller.Search(term);
            var actualProducts = new SortedSet<int>(result.Value.Select(x => x.Id));
            Assert.That(actualProducts, Is.EquivalentTo(expectedProducts));
        }

        [Test]
        public async Task When_no_product_in_db_then_CreateProduct_creates_product()
        {
            var product = _fixture.Create<Product>();

            var inputModel = _mapper.Map<AddProductInputModel>(product);
            var result = await _controller.CreateProduct(inputModel) as CreatedAtActionResult;

            product = await _dbContext.Products.FindAsync(result.RouteValues["id"]);
            Assert.That(product.Name, Is.EqualTo(inputModel.Name));
        }

        [Test]
        public async Task When_DeleteProduct_called_then_product_removed_from_DB()
        {
            var product = _products[1];

            await _controller.DeleteProduct(product.Id);

            product = await _dbContext.Products.FindAsync(product.Id);
            Assert.That(product, Is.Null);
        }
    }
}
