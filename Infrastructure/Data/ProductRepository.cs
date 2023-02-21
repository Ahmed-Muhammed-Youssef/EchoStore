using Core.Entities;
using Core.Interfaces;
using Core.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public async Task<ProductInfo> AddProductInfo(ProductInfo productInfo)
        {
            var d = await _storeContext.ProductInfo.AddAsync(productInfo);
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

        public async Task<ProductInfo> DeleteProductInfo(ProductInfo productInfo)
        {
            var d = _storeContext.ProductInfo.Remove(productInfo);
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

        public async Task<ProductInfo> GetProductInfoById(int id)
        {
            var product = await _storeContext.ProductInfo
                .AsNoTracking()
                .Include(p => p.ProductBrand)
                .Include(P => P.ProductType)
                .FirstOrDefaultAsync(P => P.Id == id);
            return product;
        }

        public IReadOnlyList<ProductInfo> GetProductsInfo(Expression<Func<ProductInfo, bool>> filter,
            Expression<Func<ProductInfo, object>>orderBy, ref PaginationInfo paginationInfo)
        {
            if(orderBy == null)
            {
                orderBy = (p => p.Id);
            }
            var productsQuery = _storeContext.ProductInfo
                .AsNoTracking()
                .Include(p => p.ProductBrand)
                .Include(P => P.ProductType)
                .Where(filter.Compile())
                .OrderBy(orderBy.Compile());
            var pCount = productsQuery.Count();
            paginationInfo.NumberOfItems = pCount;
            var products = productsQuery.Skip(paginationInfo.GetFirstElementIndex())
                .Take(paginationInfo.PageSize).ToList();
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

        public async Task<int> UpdateProductInfo(ProductInfo product)
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
