using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Catalog.Api.DomainModel;
using Catalog.Api.Infrastructure;
using Catalog.Api.InputModels;
using Catalog.Api.Validators;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Catalog.Api.UnitTests.Validators
{
    [TestFixture]
    public class AddProductModelValidatorTests
    {
        private Fixture _fixture;
        private CatalogContext _dbContext;
        private List<Product> _products;
        private AddProductModelValidator _validator;
        private AddProductInputModel _model;

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

            _model = _fixture.Create<AddProductInputModel>();
            
            _validator = new AddProductModelValidator(_dbContext);
        }

        [Test]
        public async Task When_no_name_then_return_error()
        {
            _model.Name = null;

            _validator.ShouldHaveValidationErrorFor(x => x.Name, _model);
        }
        
        [Test]
        public async Task When_no_code_then_return_error()
        {
            _model.Code = null;

            _validator.ShouldHaveValidationErrorFor(x => x.Code, _model);
        }
        
        [Test]
        public async Task When_no_code_not_unique_then_return_error()
        {
            _model.Code = _products.First().Code;

            _validator.ShouldHaveValidationErrorFor(x => x.Code, _model);
        }
        
        [Test]
        public async Task When_price_less_than_0_then_return_error()
        {
            _model.Price = -1;

            _validator.ShouldHaveValidationErrorFor(x => x.Price, _model);
        }
        
        [Test]
        public async Task When_price_greater_999_than_should_be_confirmed()
        {
            _model.Price = 999.1m;
            _model.ConfirmPrice = false;

            _validator.ShouldHaveValidationErrorFor(x => x.ConfirmPrice, _model);
        }
    }
}
