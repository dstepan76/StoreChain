using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<Product> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<Product> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                return Ok(products);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByIdAsync(int productId)
        {
            try
            {
                var product = await _productService.GetAsync(productId);
                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] Product product)
        {
            try
            {
                product = await _productService.InsertAsync(product);
                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Product product)
        {
            try
            {
                product = await _productService.UpdateAsync(product);
                return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            try
            {
                await _productService.DeleteAsync(productId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
