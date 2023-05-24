using API.DTOs;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces.Services;
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
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> Get()
        {
            var orders = await _orderService.GetOrdersForUserAsync(HttpContext.User.RetrieveEmailFromPrincipal());
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }

        // GET api/<OrderController>/5
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderToReturnDto>> Get(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId, HttpContext.User.RetrieveEmailFromPrincipal());
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
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

        // GET: api/<OrderController>/deliveryMethods
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var dms = await _orderService.GetDeliveryMethodsAsync();
            return Ok(dms);
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
