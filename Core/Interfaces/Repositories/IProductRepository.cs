using Core.Entities;
using Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        // CRUD products
        Task<Product> AddProductAsync(Product product);
        void RemoveProduct(Product product);
        Task<IReadOnlyList<Product>> GetProductsByProductInfoIdAsync(int productInfoId, int quantity);
        Task<int> GetProductsCountPerProductInfoAsync(int productInfoId);
        Task<ProductInfo> AddProductInfoAsync(ProductInfo product);
        Task<ProductInfo> GetProductInfoByIdAsync(int id);
        void DeleteProductInfo(ProductInfo productInfo);
        void DeleteProduct(Product product);
        void UpdateProductInfo(ProductInfo product);
        IReadOnlyList<ProductInfo> GetProductsInfo(Expression<Func<ProductInfo, bool>> filter,
            Expression<Func<ProductInfo, object>> orderBy, ref PaginationInfo paginationInfo);
        
        // CRUD product types
        Task<ProductType> AddProductTypeAsync(ProductType productType);
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<ProductType> GetProductTypeAsync(int id);
        void DeleteProductType(ProductType productType);
        void UpdateProductType(ProductType productType);
    }
}