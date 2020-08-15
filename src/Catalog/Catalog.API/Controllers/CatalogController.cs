using Catalog.API.Entities;
using Catalog.API.Repositories.Interface;
using DnsClient.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);

        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ActionName("GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id :{id},not found");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("{productname:length(100)}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string productname)
        {
            var product = await _productRepository.GetProductByName(productname);
            if (product == null)
            {
                _logger.LogError($"Product with Name :{productname},not found");
                return NotFound();
            }
            return Ok(product);
        }

        //[Route("GetProductByCategory/{category}")]
        [Route("[action]/{category}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryName)
        {
            var product = await _productRepository.GetProductByCategory(categoryName);
            if (product == null)
            {
                _logger.LogError($"Product with Catagory Name :{categoryName},not found");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProducts([FromBody] Product product)
        {
            await _productRepository.Create(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProducts([FromBody] Product product)
        {
           return Ok(await _productRepository.Update(product));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProducts(string id)
        {
            return Ok(await _productRepository.Delete(id));
        }
    }
}
