using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly StoreContext _storeContext;

        public BrandRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<ProductBrand> AddProductBrandAsync(ProductBrand productBrand)
        {
            var d = await _storeContext.ProductBrands.AddAsync(productBrand);
            return d.Entity;
        }
        public void DeleteBrand(ProductBrand productBrand)
        {
            var d = _storeContext.ProductBrands.Remove(productBrand);
        }
        public async Task<ProductBrand> GetBrandAsync(int id) => await _storeContext.ProductBrands
            .AsNoTracking()
            .FirstOrDefaultAsync(brand => brand.Id == id);
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync() => await _storeContext.ProductBrands
            .AsNoTracking()
            .ToListAsync();
        public void UpdateBrand(ProductBrand productBrand)
        {
            if (productBrand is null)
            {
                throw new ArgumentNullException(nameof(productBrand));
            }

            _storeContext.Entry(productBrand).State = EntityState.Modified;
        }
    }
}
