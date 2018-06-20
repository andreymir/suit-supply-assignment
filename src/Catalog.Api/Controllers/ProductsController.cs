using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Api.DomainModel;
using Catalog.Api.Infrastructure;
using Catalog.Api.InputModels;
using Catalog.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CatalogContext _catalogContext;
        private readonly IMapper _mapper;

        public ProductsController(CatalogContext catalogContext, IMapper mapper)
        {
            _catalogContext = catalogContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ProductViewModel>>> Search(string term)
        {
            var products = await _catalogContext.Products
                .Where(x => x.Name.StartsWith(term) || x.Code.StartsWith(term))
                .ToListAsync();
            
            var viewModel = products
                .Select(x => _mapper.Map<ProductViewModel>(x))
                .ToList();
            
            return viewModel;
        }
        
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductViewModel),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductViewModel>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var product = await _catalogContext.Products.SingleOrDefaultAsync(ci => ci.Id == id);

            if (product != null)
            {
                var viewModel = _mapper.Map<Product, ProductViewModel>(product);
                return viewModel;
            }

            return NotFound();
        }
        
        //PUT api/v1/products
        [Route("")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateProduct([FromBody]UpdateProductInputModel inputModel)
        {
            var product = await _catalogContext.Products
                .SingleOrDefaultAsync(i => i.Id == inputModel.Id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product {inputModel.Id} is not found." });
            }

            // Update current product
            _mapper.Map(inputModel, product);
            _catalogContext.Products.Update(product);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, null);
        }
        
        //POST api/v1/products
        [Route("")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody]AddProductInputModel inputModel)
        {
            var product = new Product();
            _mapper.Map(inputModel, product);
            _catalogContext.Products.Add(product);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, null);
        }
        
        //DELETE api/v1/catalog/products/id
        [Route("{id:int}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _catalogContext.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return Ok();
            }

            _catalogContext.Products.Remove(product);

            await _catalogContext.SaveChangesAsync();

            return Ok();
        }
    }
}