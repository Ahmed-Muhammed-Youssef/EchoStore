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
            string search = "", int pageNumber = 1, int pageSize = 4)
        {
            var pInfo = new PaginationInfo(pageSize: pageSize - 1, currentPageNumber: pageNumber);
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
         /*   HttpContext.Response.Headers.Add("PaginationNumberOfItems", pInfo.NumberOfItems.ToString());
            HttpContext.Response.Headers.Add("PaginationPageNumber", pInfo.CurrentPageNumber.ToString());
            HttpContext.Response.Headers.Add("PaginationPageSize", pInfo.PageSize.ToString());
            HttpContext.Response.Headers.Add("PaginationLastPage", pInfo.LastPage.ToString());*/
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
    }
}