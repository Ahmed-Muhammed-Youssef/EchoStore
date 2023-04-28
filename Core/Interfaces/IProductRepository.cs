using Core.Entities;
using Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        // CRUD products
        Task<Product> AddProductAsync(Product product);
        Task<Product> RemoveProductAsync(Product product);
        Task<IReadOnlyList<Product>> GetProductsByProductInfoIdAsync(int productInfoId, int quantity);
        Task<int> GetProductsCountPerProductInfoAsync(int productInfoId);
        Task<ProductInfo> AddProductInfoAsync(ProductInfo product);
        Task<ProductInfo> GetProductInfoByIdAsync(int id);
        Task<ProductInfo> DeleteProductInfoAsync(ProductInfo productInfo);
        Task<Product> DeleteProductAsync(Product product);
        Task<int> UpdateProductInfoAsync(ProductInfo product);
        IReadOnlyList<ProductInfo> GetProductsInfo(Expression<Func<ProductInfo, bool>> filter,
            Expression<Func<ProductInfo, object>> orderBy, ref PaginationInfo paginationInfo);
        // CRUD brands
        Task<ProductBrand> AddProductBrandAsync(ProductBrand productBrand);
        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        Task<ProductBrand> GetBrandAsync(int id);
        Task<ProductBrand> DeleteBrandAsync(ProductBrand productBrand);
        Task<int> UpdateBrandAsync(ProductBrand productBrand);
        // CRUD product types
        Task<ProductType> AddProductTypeAsync(ProductType productType);
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<ProductType> GetProductTypeAsync(int id);
        Task<ProductType> DeleteProductTypeAsync(ProductType productType);
        Task<int> UpdateProductTypeAsync(ProductType productType);
    }
}