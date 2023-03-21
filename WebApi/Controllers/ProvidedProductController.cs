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
    public class ProvidedProvidedProductController : ControllerBase
    {
        private readonly ILogger<ProvidedProduct> _logger;
        private readonly IProvidedProductService _providedProductService;

        public ProvidedProvidedProductController(ILogger<ProvidedProduct> logger, IProvidedProductService providedProductService)
        {
            _logger = logger;
            _providedProductService = providedProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var providedProducts = await _providedProductService.GetAllAsync();
                return Ok(providedProducts);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{productId}/{salesPointId}")]
        public async Task<IActionResult> GetByIdAsync(int productId, int salesPointId)
        {
            try
            {
                var providedProduct = await _providedProductService.GetAsync(productId, salesPointId);
                return Ok(providedProduct);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] ProvidedProduct providedProduct)
        {
            try
            {
                providedProduct = await _providedProductService.InsertAsync(providedProduct);
                return Ok(providedProduct);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProvidedProduct providedProduct)
        {
            try
            {
                providedProduct = await _providedProductService.UpdateAsync(providedProduct);
                return Ok(providedProduct);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{productId}/{salesPointId}")]
        public async Task<IActionResult> DeleteAsync(int productId, int salesPointId)
        {
            try
            {
                await _providedProductService.DeleteAsync(productId, salesPointId);
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
