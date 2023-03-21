using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleOperationController : ControllerBase
    {
        private readonly ILogger<SaleOperationController> _logger;
        private readonly ISaleOperationService _saleOperationService;

        public SaleOperationController(ILogger<SaleOperationController> logger, ISaleOperationService saleOperationService)
        {
            _logger = logger;
            _saleOperationService = saleOperationService;
        }

        public class SaleOperationRequest
        {
            public int SalesPointId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public int? BuyerId { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> SaleOperationAsync([FromBody] SaleOperationRequest request)
        {
            try
            {
                await _saleOperationService.SaleAsync(request.SalesPointId, request.BuyerId, request.ProductId, request.Quantity);
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
