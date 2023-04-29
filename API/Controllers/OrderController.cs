﻿using API.DTOs;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> Get()
        {
            var orders = await _orderService.GetOrdersForUserAsync(HttpContext.User.RetrieveEmailFromPrincipal());
            return Ok(orders);
        }

        // GET api/<OrderController>/5
        [HttpGet("{cartId}")]
        public async Task<ActionResult<Order>> Get(string cartId)
        {
            var order = await _orderService.GetOrderByIdAsync(cartId, HttpContext.User.RetrieveEmailFromPrincipal());
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] OrderDto orderDto)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();
            var address = _mapper.Map<Address>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.CartId, address);
            if(order == null)
            {
                return BadRequest("Order creation failed");
            }
            return CreatedAtAction(nameof(Get), new {order.Id}, order);
        }
        // @ToDo: Cancel Order endpoint
        // DELETE api/<OrderController>/5
       /* [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _orderService.
        }*/
    }
}