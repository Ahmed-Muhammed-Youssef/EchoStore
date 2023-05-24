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
            var pi = await _storeContext.ProductInfo.Where(p => p.Id == product.ProductInfoId).FirstOrDefaultAsync();
            IncrementProductInfoAmount(pi);
            return d.Entity;
        }
        public async Task RemoveProductAsync(Product product)
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
            var pi = product.ProductInfo;
            if(pi == null)
            {
                pi = await _storeContext.ProductInfo.FindAsync(product.ProductInfoId);
            }
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
            // must be 0 to pervent invalid object states in the database.
            productInfo.AvailableAmount = 0;
            var d = await _storeContext.ProductInfo.AddAsync(productInfo);
            return d.Entity;
        }
        
        public async Task<int> GetProductsCountPerProductInfoAsync(int productInfoId)
        {
            return await _storeContext.Products
                .Where(p => p.ProductInfoId == productInfoId)
                .CountAsync();
        }
       
        public void DeleteProductInfo(ProductInfo productInfo)
        {
            var d = _storeContext.ProductInfo.Remove(productInfo);
        }
        public void DeleteProduct(Product product)
        {
            var d = _storeContext.Products.Remove(product);
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
        
        public void UpdateProductInfo(ProductInfo product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _storeContext.Entry(product).State = EntityState.Modified;
        }
       
        public async Task<bool> AnyProductInfoAsync()
        {
            return await _storeContext.ProductInfo.AnyAsync();
        } 
        public async Task<bool> AnyProductAsync()
        {
            return await _storeContext.Products.AnyAsync();
        } 
        // Utitlity
        private void IncrementProductInfoAmount(ProductInfo productInfo)
        {
            productInfo.AvailableAmount++;
            UpdateProductInfo(productInfo);
        }
        private void DecrementProductInfoAmount(ProductInfo productInfo)
        {
            if (productInfo.AvailableAmount > 1) { productInfo.AvailableAmount--; }
            UpdateProductInfo(productInfo);
        }

    }
}
