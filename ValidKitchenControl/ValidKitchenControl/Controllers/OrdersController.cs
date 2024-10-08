﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidKitchenControl.Domain.Entities;
using ValidKitchenControl.Domain.Services;

namespace ValidKitchenControl.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order[] orders)
        {
            foreach (var order in orders)
            {
                await _orderService.CreateAsync(order);
            }

            return CreatedAtAction(nameof(CreateOrder), new { id = orders[0].Id }, orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }
    }
}
