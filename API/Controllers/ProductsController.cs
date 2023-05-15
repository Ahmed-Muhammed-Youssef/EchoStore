using API.DTOs;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // GET: api/products
        [Cached(1000)]
        [HttpGet]
        public ActionResult<List<ProductInfoDto>> GetProductsInfo(string sortBy, int? brandId, int? typeId,
            string search = "", [FromHeader] int pageNumber = 0, [FromHeader] int pageSize = 4)
        {
            var pInfo = new PaginationInfo(pageSize: pageSize, currentPageNumber: pageNumber);
            search = search.ToLower();
            Expression<Func<ProductInfo, object>> orderBy = p => p.Name;
            Expression<Func<ProductInfo, bool>> filter = p => true;
            if(brandId != null || typeId != null || !string.IsNullOrEmpty(search))
            {
                filter = p => (brandId == null || brandId == p.ProductBrandId) &&
                (typeId == null || typeId == p.ProductTypeId) && (search == "" || p.Name.ToLower().Contains(search) 
                || p.Description.ToLower().Contains(search));
            }
            switch (sortBy)
            {
                case "name":
                    orderBy = p => p.Name;
                    break;
                case "price":
                    orderBy = p => p.Price;
                    break;
                case "rate":
                    orderBy = p => p.Rate;
                    break;
                default:
                    break;
            }
            var products = _unitOfWork.ProductRepository.GetProductsInfo(
                filter: filter,
                orderBy: orderBy,
                paginationInfo: ref pInfo
            );
            HttpContext.Response.Headers.Add("PaginationNumberOfItems", pInfo.NumberOfItems.ToString());
            HttpContext.Response.Headers.Add("PaginationPageNumber", pInfo.CurrentPageNumber.ToString());
            HttpContext.Response.Headers.Add("PaginationPageSize", pInfo.PageSize.ToString());
            HttpContext.Response.Headers.Add("PaginationLastPage", pInfo.LastPage.ToString());
            return Ok(products.Select(p => mapper.Map<ProductInfo, ProductInfoDto>(p)));
        }
        // GET: api/products/5
        [Cached(1000)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInfoDto>> GetProductInfo(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductInfoByIdAsync(id);
            if (product == null) {
                return NotFound();
            }
            return Ok(mapper.Map<ProductInfo, ProductInfoDto>(product));
        }
        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductInfo(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductInfoByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.DeleteProductInfo(product);
            var res = await _unitOfWork.Complete();
            if(res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return NoContent();
        }
        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductBrand>> AddProductInfo(ProductInfo product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            product = await _unitOfWork.ProductRepository.AddProductInfoAsync(product);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return Ok(product);
        }
        // PUT: api/products
        [HttpPut]
        public async Task<ActionResult<ProductBrand>> UpdateProductInfo(ProductInfo product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            if (await _unitOfWork.ProductRepository.GetProductInfoByIdAsync(product.Id) == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.UpdateProductInfo(product);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return Ok(product);
        }

        /// <summary>
        ///  Crud APIs for brands
        /// </summary>
        // GET: api/products/brands
        [Cached(1000)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _unitOfWork.ProductRepository.GetBrandsAsync();
            return Ok(brands);
        }
        // GET: api/products/brands/5
        [Cached(1000)]
        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            var productBrand = await _unitOfWork.ProductRepository.GetBrandAsync(id);

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
            var brand = await _unitOfWork.ProductRepository.GetBrandAsync(id);

            if (brand == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.DeleteBrand(brand);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
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
            var brand = await _unitOfWork.ProductRepository.AddProductBrandAsync(newProductBrand);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
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

            if (await _unitOfWork.ProductRepository.GetBrandAsync(brand.Id) == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.UpdateBrand(brand);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return Ok(brand);
        }

        /// <summary>
        ///  Crud APIs for product types
        /// </summary>
        // GET: api/products/types
        [HttpGet("types")]
        [Cached(1000)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            var types = await _unitOfWork.ProductRepository.GetProductTypesAsync();
            return Ok(types);
        }
        // GET: api/products/types/5
        [HttpGet("types/{id}")]
        [Cached(1000)]
        public async Task<ActionResult<ProductBrand>> GetProductType(int id)
        {
            var productType = await _unitOfWork.ProductRepository.GetProductTypeAsync(id);

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
            var type = await _unitOfWork.ProductRepository.GetProductTypeAsync(id);

            if (type == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.DeleteProductType(type);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
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
            type = await _unitOfWork.ProductRepository.AddProductTypeAsync(type);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
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

            if (await _unitOfWork.ProductRepository.GetProductTypeAsync(type.Id) == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.UpdateProductType(type);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return Ok(type);
        }
    }
}