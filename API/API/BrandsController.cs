using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;
using Asp.Helpers;
using Core.Interfaces;
using System;

namespace Asp.API
{
    /// <summary>
    ///  Crud APIs for brands
    /// </summary>
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/brands
        [ApiVersion("1.0")]
        [Cached(1000)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsV1()
        {
            var brands = await _unitOfWork.BrandRepository.GetBrandsAsync();
            return Ok(brands);
        }
        [ApiVersion("2.0")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsV2()
        {
            var brands = await _unitOfWork.BrandRepository.GetBrandsAsync();
            return Ok(brands.Where(b => b.Name.StartsWith('S')));
        }
        // GET: api/brands/5
        [Cached(1000)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            var productBrand = await _unitOfWork.BrandRepository.GetBrandAsync(id);

            if (productBrand == null)
            {
                return NotFound();
            }

            return Ok(productBrand);
        }
        // DELETE: api/brands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductBrand(int id)
        {
            var brand = await _unitOfWork.BrandRepository.GetBrandAsync(id);

            if (brand == null)
            {
                return NotFound();
            }
            _unitOfWork.BrandRepository.DeleteBrand(brand);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return NoContent();
        }
        // POST: api/brands
        [HttpPost("brands")]
        public async Task<ActionResult<ProductBrand>> AddProductBrands(ProductBrand newProductBrand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            var brand = await _unitOfWork.BrandRepository.AddProductBrandAsync(newProductBrand);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return Ok(brand);
        }
        // PUT: api/brands
        [HttpPut("brands")]
        public async Task<ActionResult<ProductBrand>> UpdateBrand(ProductBrand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            if (await _unitOfWork.BrandRepository.GetBrandAsync(brand.Id) == null)
            {
                return NotFound();
            }
            _unitOfWork.BrandRepository.UpdateBrand(brand);
            var res = await _unitOfWork.Complete();
            if (res <= 0)
            {
                throw new Exception("Transaction error");
            }
            return Ok(brand);
        }
    }
}
