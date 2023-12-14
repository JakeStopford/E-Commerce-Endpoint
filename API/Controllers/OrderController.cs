using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, 
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder([FromBody] OrderToSaveDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var orderId = await _orderService.SaveOrder(order);
                return StatusCode(201, orderId);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Save order request failed with reason: " + ex.Message);
                return StatusCode(500, "An error occurred while saving the order.");
            }
        }
    }
}
