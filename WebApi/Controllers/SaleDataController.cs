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
    public class SalesDataController : ControllerBase
    {
        private readonly ILogger<SalesData> _logger;
        private readonly ISalesDataService _salesDataService;

        public SalesDataController(ILogger<SalesData> logger, ISalesDataService salesDataService)
        {
            _logger = logger;
            _salesDataService = salesDataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var salesDatas = await _salesDataService.GetAllAsync();
                return Ok(salesDatas);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{productId}/{saleId}")]
        public async Task<IActionResult> GetByIdAsync(int productId, int saleId)
        {
            try
            {
                var salesData = await _salesDataService.GetAsync(productId, saleId);
                return Ok(salesData);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] SalesData salesData)
        {
            try
            {
                salesData = await _salesDataService.InsertAsync(salesData);
                return Ok(salesData);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] SalesData salesData)
        {
            try
            {
                salesData = await _salesDataService.UpdateAsync(salesData);
                return Ok(salesData);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{productId}/{saleId}")]
        public async Task<IActionResult> DeleteAsync(int productId, int saleId)
        {
            try
            {
                await _salesDataService.DeleteAsync(productId, saleId);
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
