using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public ProductsController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        /*
        * Retrieving The Products data from the database
        * should support:
        * 1- Filtering
        * 2- Pagination
        */
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(){
            var products =  await _storeContext.Products.ToListAsync();
            return Ok(products);
        }
        /*
        * Retrieve a single product using an ID
        */
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id){
            var product = await _storeContext.Products.FindAsync(id);
            if(product == null){
                return BadRequest();
            }
            return Ok(product);
        }
    }
}