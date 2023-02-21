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
        Task<Product> AddProduct(Product product);
        Task<ProductInfo> AddProductInfo(ProductInfo product);
        Task<ProductInfo> GetProductInfoById(int id);
        Task<ProductInfo> DeleteProductInfo(ProductInfo productInfo);
        Task<Product> DeleteProduct(Product product);
        Task<int> UpdateProductInfo(ProductInfo product);
        IReadOnlyList<ProductInfo> GetProductsInfo(Expression<Func<ProductInfo, bool>> filter,
            Expression<Func<ProductInfo, object>> orderBy, ref PaginationInfo paginationInfo);
        // CRUD brands
        Task<ProductBrand> AddProductBrand(ProductBrand productBrand);
        Task<IReadOnlyList<ProductBrand>> GetBrands();
        Task<ProductBrand> GetBrand(int id);
        Task<ProductBrand> DeleteBrand(ProductBrand productBrand);
        Task<int> UpdateBrand(ProductBrand productBrand);
        // CRUD product types
        Task<ProductType> AddProductType(ProductType productType);
        Task<IReadOnlyList<ProductType>> GetProductTypes();
        Task<ProductType> GetProductType(int id);
        Task<ProductType> DeleteProductType(ProductType productType);
        Task<int> UpdateProductType(ProductType productType);
    }
}