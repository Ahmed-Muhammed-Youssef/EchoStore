using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly StoreContext _storeContext;

        public ProductTypeRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<ProductType> AddProductTypeAsync(ProductType productType)
        {
            var d = await _storeContext.ProductTypes.AddAsync(productType);
            return d.Entity;
        }

        public async Task<ProductType> GetProductTypeAsync(int id) => await _storeContext.ProductTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(type => type.Id == id);
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync() => await _storeContext.ProductTypes
            .AsNoTracking()
            .ToListAsync();

        public void DeleteProductType(ProductType productType)
        {
            var d = _storeContext.ProductTypes.Remove(productType);
        }

        public void UpdateProductType(ProductType productType)
        {
            _storeContext.Entry(productType).State = EntityState.Modified;
        }

    }
}
