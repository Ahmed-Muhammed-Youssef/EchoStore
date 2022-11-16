using Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        // CRUD products
        Task<Product> AddProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<Product> DeleteProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        IReadOnlyList<Product> GetProducts(Func<Product, bool> filter);
        // CRUD brands
        Task<ProductBrand> AddProductBrand(ProductBrand productBrand);
        Task<IReadOnlyList<ProductBrand>> GetBrands();
        Task<ProductBrand> GetBrand(int id);
        Task<ProductBrand> DeleteBrand(ProductBrand productBrand);
        Task<ProductBrand> UpdateBrand(ProductBrand productBrand);
        // CRUD product types
        Task<ProductType> AddProductType(ProductType productType);
        Task<IReadOnlyList<ProductType>> GetProductTypes();
        Task<ProductType> GetProductType(int id);
        Task<ProductType> DeleteProductType(ProductType productType);
        Task<ProductType> UpdateProductType(ProductType productType);
    }
}