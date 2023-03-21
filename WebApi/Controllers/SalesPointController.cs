using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPointController : ControllerBase
    {
        private readonly ILogger<SalesPoint> _logger;
        private readonly ISalesPointService _salesPointService;

        public SalesPointController(ILogger<SalesPoint> logger, ISalesPointService salesPointService)
        {
            _logger = logger;
            _salesPointService = salesPointService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var salesPoints = await _salesPointService.GetAllAsync();
                return Ok(salesPoints);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{salesPointId}")]
        public async Task<IActionResult> GetByIdAsync(int salesPointId)
        {
            try
            {
                var salesPoint = await _salesPointService.GetAsync(salesPointId);
                return Ok(salesPoint);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] SalesPoint salesPoint)
        {
            try
            {
                salesPoint = await _salesPointService.InsertAsync(salesPoint);
                return Ok(salesPoint);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] SalesPoint salesPoint)
        {
            try
            {
                salesPoint = await _salesPointService.UpdateAsync(salesPoint);
                return Ok(salesPoint);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{salesPointId}")]
        public async Task<IActionResult> DeleteAsync(int salesPointId)
        {
            try
            {
                await _salesPointService.DeleteAsync(salesPointId);
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
