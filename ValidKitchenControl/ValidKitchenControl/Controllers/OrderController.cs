using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Services;

namespace ValidKitchenControl.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!new[] { "fritos", "grelhados", "saladas", "bebidas", "sobremesa" }.Contains(order.Area))
            {
                return BadRequest("Área inválida.");
            }

            await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(CreateOrder), new { id = order.Id }, order);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
    }
}
