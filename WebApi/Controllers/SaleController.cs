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
    public class SaleController : ControllerBase
    {
        private readonly ILogger<Sale> _logger;
        private readonly ISaleService _saleService;

        public SaleController(ILogger<Sale> logger, ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var sales = await _saleService.GetAllAsync();
                return Ok(sales);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetByIdAsync(int saleId)
        {
            try
            {
                var sale = await _saleService.GetAsync(saleId);
                return Ok(sale);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] Sale sale)
        {
            try
            {
                sale = await _saleService.InsertAsync(sale);
                return Ok(sale);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Sale sale)
        {
            try
            {
                sale = await _saleService.UpdateAsync(sale);
                return Ok(sale);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{saleId}")]
        public async Task<IActionResult> DeleteAsync(int saleId)
        {
            try
            {
                await _saleService.DeleteAsync(saleId);
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
