using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var d = await _storeContext.Products.AddAsync(product);
            await SaveChangesAsync();
            return d.Entity;
        }

        public async Task<ProductBrand> AddProductBrand(ProductBrand productBrand)
        {
            var d = await _storeContext.ProductBrands.AddAsync(productBrand);
            await SaveChangesAsync();
            return d.Entity;
        }

        public async Task<ProductType> AddProductType(ProductType productType)
        {
            var d = await _storeContext.ProductTypes.AddAsync(productType);
            await SaveChangesAsync();
            return d.Entity;
        }

        public async Task<ProductBrand> DeleteBrand(ProductBrand productBrand)
        {
            var d =  _storeContext.ProductBrands.Remove(productBrand);
            await SaveChangesAsync();
            return d.Entity;
        }

        public async Task<Product> DeleteProduct(Product product)
        {
            var d = _storeContext.Products.Remove(product);
            await SaveChangesAsync();
            return d.Entity;
        }

        public async Task<ProductType> DeleteProductType(ProductType productType)
        {
            var d = _storeContext.ProductTypes.Remove(productType);
            await SaveChangesAsync();
            return d.Entity;
        }

        public async Task<ProductBrand> GetBrand(int id) => await _storeContext.ProductBrands
            .AsNoTracking()
            .FirstOrDefaultAsync(brand => brand.Id == id);

        public async Task<IReadOnlyList<ProductBrand>> GetBrands() => await _storeContext.ProductBrands
            .AsNoTracking()
            .ToListAsync();

        public async Task<Product> GetProductById(int id)
        {
            var product = await _storeContext.Products
                .AsNoTracking()
                .Include(p => p.ProductBrand)
                .Include(P => P.ProductType)
                .FirstOrDefaultAsync(P => P.Id == id);
            return product;
        }

        public IReadOnlyList<Product> GetProducts(Func<Product, bool> filter)
        {
            var products = _storeContext.Products
                .AsNoTracking()
                .Include(p => p.ProductBrand)
                .Include(P => P.ProductType)
                .Where(filter == null ? p => true : filter)
                .ToList<Product>();
            return products;
        }

        public async Task<ProductType> GetProductType(int id) => await _storeContext.ProductTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(type => type.Id == id);

        public async Task<IReadOnlyList<ProductType>> GetProductTypes() => await _storeContext.ProductTypes
            .AsNoTracking()
            .ToListAsync();

        public async Task<int> UpdateBrand(ProductBrand productBrand)
        {
            _storeContext.Entry(productBrand).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public async Task<int> UpdateProduct(Product product)
        {
            _storeContext.Entry(product).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public async Task<int> UpdateProductType(ProductType productType)
        {
            _storeContext.Entry(productType).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
        private async Task<int> SaveChangesAsync()
        {
            int changes = 0;
            try
            {
                changes = await _storeContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return changes;
        }
    }
}
