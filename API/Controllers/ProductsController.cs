using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public ActionResult<List<Product>> GetProducts()
        {
            var products = _productRepository.GetProducts(filter: p => true);
            return Ok(products);
        }
        /*
        * Retrieve a single product using an ID
        */
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if(product == null){
                return NotFound();
            }
            return Ok(product);
        }


        /// <summary>
        ///  Crud APIs for brands
        /// </summary>
        [Route("brands")]
        // GET: api/products/brands
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productRepository.GetBrands();
            return Ok(brands);
        }
        // GET: api/products/brands/5
        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            var productBrand = await _productRepository.GetBrand(id);

            if (productBrand == null)
            {
                return NotFound();
            }

            return Ok(productBrand);
        }
        /// <summary>
        ///  Crud APIs for product types
        /// </summary>
        // GET: api/products/types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            var types = await _productRepository.GetProductTypes();
            return Ok(types);
        }
        // GET: api/products/types/5
        [HttpGet("types/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductType(int id)
        {
            var productType = await _productRepository.GetProductType(id);

            if (productType == null)
            {
                return NotFound();
            }

            return Ok(productType);
        }
    }
}