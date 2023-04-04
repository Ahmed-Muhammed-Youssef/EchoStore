﻿using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            this._cartRepository = cartRepository;
            this._mapper = mapper;
        }
        // GET: api/Cart/string
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart([FromQuery(Name ="id")]string id)
        {
            var cart = await _cartRepository.GetCart(id);
            if(cart == null)
            {
                return NotFound("The cart is not found.");
            }
            return Ok(cart);
        }

        // POST api/Cart
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CartDto cartDto)
        {
            var cart = _mapper.Map<CartDto, Cart>(cartDto);
            if(await _cartRepository.CreateUpdateCart(cart))
            {
                return CreatedAtAction("Create", new { id = cart.Id }, cart);
            }
            return BadRequest();
        }

        // PUT api/Cart/string
        [HttpPut]
        public async Task<IActionResult> PutCart([FromQuery]string id, [FromBody] CartDto cartDto)
        {
            var cart = _mapper.Map<CartDto, Cart>(cartDto);
            if (id != cart.Id)
            {
                return BadRequest();
            }
            if(await _cartRepository.GetCart(id) == null)
            {
                return NotFound("The cart is not found.");
            }
            if (await _cartRepository.CreateUpdateCart(cart))
            {
                return CreatedAtAction("Create", new { id = cart.Id }, cart);
            }
            return BadRequest();
        }

        // DELETE api/Cart/string
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]string id)
        {
            var cart = await _cartRepository.GetCart(id);
            if(cart == null)
            {
                return NotFound("The cart is not found.");
            }
            if(await _cartRepository.DeleteCart(cart))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
