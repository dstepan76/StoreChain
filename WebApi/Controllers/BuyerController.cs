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
    public class BuyerController : ControllerBase
    {
        private readonly ILogger<Buyer> _logger;
        private readonly IBuyerService _buyerService;

        public BuyerController(ILogger<Buyer> logger, IBuyerService buyerService)
        {
            _logger = logger;
            _buyerService = buyerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var buyers = await _buyerService.GetAllAsync();
                return Ok(buyers);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{buyerId}")]
        public async Task<IActionResult> GetByIdAsync(int buyerId)
        {
            try
            {
                var buyer = await _buyerService.GetAsync(buyerId);
                return Ok(buyer);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] Buyer buyer)
        {
            try
            {
                buyer = await _buyerService.InsertAsync(buyer);
                return Ok(buyer);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Buyer buyer)
        {
            try
            {
                buyer = await _buyerService.UpdateAsync(buyer);
                return Ok(buyer);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{buyerId}")]
        public async Task<IActionResult> DeleteAsync(int buyerId)
        {
            try
            {
                await _buyerService.DeleteAsync(buyerId);
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
