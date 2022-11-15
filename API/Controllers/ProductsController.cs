using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        /*
        * Retrieving The Products data from the database
        * should support:
        * 1- Filtering
        * 2- Pagination
        */
        [HttpGet]
        public ActionResult<List<Product>> GetProducts(){
            var products = _productRepository.GetProducts(filter: p => true);
            return Ok(products);
        }
        /*
        * Retrieve a single product using an ID
        */
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
            var product = await _productRepository.GetProductById(id);
            if(product == null){
                return BadRequest();
            }
            return Ok(product);
        }
    }
}