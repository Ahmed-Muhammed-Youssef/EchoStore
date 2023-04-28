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
        public async Task<Product> AddProductAsync(Product product)
        {
            var d = await _storeContext.Products.AddAsync(product);
            var pi = await _storeContext.Products.Where(p => p.Id == d.Entity.Id).Select(p => p.ProductInfo).FirstOrDefaultAsync();
            await IncrementProductInfoAmountAndSaveChanges(pi);
            return d.Entity;
        }
        public async Task<Product> RemoveProductAsync(Product product)
        {
            var d = _storeContext.Products.Remove(product);
            var pi = await _storeContext.Products.Where(p => p.Id == d.Entity.Id).Select(p => p.ProductInfo).FirstOrDefaultAsync();
            await DecrementProductInfoAmountAndSaveChanges(pi);
            return d.Entity;
        }
        public async Task<IReadOnlyList<Product>> GetProductsByProductInfoIdAsync(int productInfoId, int quantity)
        {
            return await _storeContext.Products
                .Where(p => p.ProductInfoId == productInfoId)
                .Take(quantity)
                .ToListAsync();
        }
        public async Task<ProductInfo> AddProductInfoAsync(ProductInfo productInfo)
        {
            var d = await _storeContext.ProductInfo.AddAsync(productInfo);
            await SaveChangesAsync();
            return d.Entity;
        }
        public async Task<ProductBrand> AddProductBrandAsync(ProductBrand productBrand)
        {
            var d = await _storeContext.ProductBrands.AddAsync(productBrand);
            await SaveChangesAsync();
            return d.Entity;
        }
        public async Task<int> GetProductsCountPerProductInfoAsync(int productInfoId)
        {
            return await _storeContext.Products
                .Where(p => p.ProductInfoId == productInfoId)
                .CountAsync();
        }
        public async Task<ProductType> AddProductTypeAsync(ProductType productType)
        {
            var d = await _storeContext.ProductTypes.AddAsync(productType);
            await SaveChangesAsync();
            return d.Entity;
        }
        public async Task<ProductBrand> DeleteBrandAsync(ProductBrand productBrand)
        {
            var d = _storeContext.ProductBrands.Remove(productBrand);
            await SaveChangesAsync();
            return d.Entity;
        }
        public async Task<ProductInfo> DeleteProductInfoAsync(ProductInfo productInfo)
        {
            var d = _storeContext.ProductInfo.Remove(productInfo);
            await SaveChangesAsync();
            return d.Entity;
        }
        public async Task<Product> DeleteProductAsync(Product product)
        {
            var d = _storeContext.Products.Remove(product);
            await SaveChangesAsync();
            return d.Entity;
        }
        public async Task<ProductType> DeleteProductTypeAsync(ProductType productType)
        {
            var d = _storeContext.ProductTypes.Remove(productType);
            await SaveChangesAsync();
            return d.Entity;
        }
        public async Task<ProductBrand> GetBrandAsync(int id) => await _storeContext.ProductBrands
            .AsNoTracking()
            .FirstOrDefaultAsync(brand => brand.Id == id);
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync() => await _storeContext.ProductBrands
            .AsNoTracking()
            .ToListAsync();
        public async Task<ProductInfo> GetProductInfoByIdAsync(int id)
        {
            var product = await _storeContext.ProductInfo
                .AsNoTracking()
                .Include(p => p.ProductBrand)
                .Include(P => P.ProductType)
                .FirstOrDefaultAsync(P => P.Id == id);
            return product;
        }
        public IReadOnlyList<ProductInfo> GetProductsInfo(Expression<Func<ProductInfo, bool>> filter,
            Expression<Func<ProductInfo, object>> orderBy, ref PaginationInfo paginationInfo)
        {
            if (orderBy == null)
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
        public async Task<ProductType> GetProductTypeAsync(int id) => await _storeContext.ProductTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(type => type.Id == id);
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync() => await _storeContext.ProductTypes
            .AsNoTracking()
            .ToListAsync();
        public async Task<int> UpdateBrandAsync(ProductBrand productBrand)
        {
            _storeContext.Entry(productBrand).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
        public async Task<int> UpdateProductInfoAsync(ProductInfo product)
        {
            _storeContext.Entry(product).State = EntityState.Modified;
            return await SaveChangesAsync();
        }
        public async Task<int> UpdateProductTypeAsync(ProductType productType)
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
        // Utitlity
        private async Task IncrementProductInfoAmountAndSaveChanges(ProductInfo productInfoId)
        {
            productInfoId.AvailableAmount++;
            await this.UpdateProductInfoAsync(productInfoId);
        }
        private async Task DecrementProductInfoAmountAndSaveChanges(ProductInfo productInfoId)
        {
            if (productInfoId.AvailableAmount > 1) { productInfoId.AvailableAmount--; }
            await this.UpdateProductInfoAsync(productInfoId);
        }

    }
}
