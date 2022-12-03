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
        // GET: api/products
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            var products = _productRepository.GetProducts(filter: p => true);
            return Ok(products);
        }
        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null) {
                return NotFound();
            }
            return Ok(product);
        }
        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteProduct(product);
            return NoContent();
        }
        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductBrand>> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            product = await _productRepository.AddProduct(product);
            return Ok(product);
        }
        // PUT: api/products
        [HttpPut]
        public async Task<ActionResult<ProductBrand>> UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            if (await _productRepository.GetProductById(product.Id) == null)
            {
                return NotFound();
            }
            await _productRepository.UpdateProduct(product);
            return Ok(product);
        }

        /// <summary>
        ///  Crud APIs for brands
        /// </summary>
        // GET: api/products/brands
        [HttpGet("brands")]
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
        // DELETE: api/products/brands/5
        [HttpDelete("brands/{id}")]
        public async Task<ActionResult> DeleteProductBrand(int id)
        {
            var brand = await _productRepository.GetBrand(id);

            if (brand == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteBrand(brand);
            return NoContent();
        }
        // POST: api/products/brands
        [HttpPost("brands")]
        public async Task<ActionResult<ProductBrand>> AddProductBrands(ProductBrand newProductBrand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            var brand = await _productRepository.AddProductBrand(newProductBrand);
            return Ok(brand);
        }
        // PUT: api/products/brands
        [HttpPut("brands")]
        public async Task<ActionResult<ProductBrand>> UpdateBrand(ProductBrand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (await _productRepository.GetBrand(brand.Id) == null)
            {
                return NotFound();
            }
            await _productRepository.UpdateBrand(brand);
            return Ok(brand);
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
        // DELETE: api/products/types/5
        [HttpDelete("types/{id}")]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            var type = await _productRepository.GetProductType(id);

            if (type == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteProductType(type);
            return NoContent();
        }
        // POST: api/products/types
        [HttpPost("types")]
        public async Task<ActionResult<ProductType>> AddProductType(ProductType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            type = await _productRepository.AddProductType(type);
            return Ok(type);
        }
        // PUT: api/products/types
        [HttpPut("types")]
        public async Task<ActionResult<ProductBrand>> UpdateType(ProductType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (await _productRepository.GetProductType(type.Id) == null)
            {
                return NotFound();
            }
           await _productRepository.UpdateProductType(type);
            return Ok(type);
        }
    }
}