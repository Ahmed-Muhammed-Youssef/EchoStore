using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using API.Helpers;
using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace API.Controllers
{
    /// <summary>
    ///  Crud APIs for ProductTypes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/products/types
        [HttpGet]
        [Cached(1000)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            var types = await _unitOfWork.ProductTypeRepository.GetProductTypesAsync();
            return Ok(types);
        }
        // GET: api/products/types/5
        [HttpGet("{id}")]
        [Cached(1000)]
        public async Task<ActionResult<ProductBrand>> GetProductType(int id)
        {
            var productType = await _unitOfWork.ProductTypeRepository.GetProductTypeAsync(id);

            if (productType == null)
            {
                return NotFound();
            }

            return Ok(productType);
        }
        // DELETE: api/products/types/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            var type = await _unitOfWork.ProductTypeRepository.GetProductTypeAsync(id);

            if (type == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductTypeRepository.DeleteProductType(type);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return NoContent();
        }
        // POST: api/products/types
        [HttpPost]
        public async Task<ActionResult<ProductType>> AddProductType(ProductType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            type = await _unitOfWork.ProductTypeRepository.AddProductTypeAsync(type);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return Ok(type);
        }
        // PUT: api/products/types
        [HttpPut]
        public async Task<ActionResult<ProductBrand>> UpdateType(ProductType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (await _unitOfWork.ProductTypeRepository.GetProductTypeAsync(type.Id) == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductTypeRepository.UpdateProductType(type);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return Ok(type);
        }
    }
}
