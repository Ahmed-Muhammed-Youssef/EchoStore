using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
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
            IncrementProductInfoAmount(pi);
            return d.Entity;
        }
        public void RemoveProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            if (product.ProductInfo is null)
            {
                throw new ArgumentNullException(nameof(product.ProductInfo));
            }

            var d = _storeContext.Products.Remove(product);
            DecrementProductInfoAmount(product.ProductInfo);
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
            return d.Entity;
        }
        
        public void DeleteProductInfo(ProductInfo productInfo)
        {
            var d = _storeContext.ProductInfo.Remove(productInfo);
        }
        public void DeleteProduct(Product product)
        {
            var d = _storeContext.Products.Remove(product);
        }
        public void DeleteProductType(ProductType productType)
        {
            var d = _storeContext.ProductTypes.Remove(productType);
        }
        
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
                orderBy = p => p.Id;
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
       
        public void UpdateProductInfo(ProductInfo product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _storeContext.Entry(product).State = EntityState.Modified;
        }
        public void UpdateProductType(ProductType productType)
        {
            _storeContext.Entry(productType).State = EntityState.Modified;
        }

        // Utitlity
        private void IncrementProductInfoAmount(ProductInfo productInfoId)
        {
            productInfoId.AvailableAmount++;
            UpdateProductInfo(productInfoId);
        }
        private void DecrementProductInfoAmount(ProductInfo productInfoId)
        {
            if (productInfoId.AvailableAmount > 1) { productInfoId.AvailableAmount--; }
            UpdateProductInfo(productInfoId);
        }

    }
}
