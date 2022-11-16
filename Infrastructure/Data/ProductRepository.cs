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

        public Task<Product> AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductBrand> AddProductBrand(ProductBrand productBrand)
        {
            throw new NotImplementedException();
        }

        public Task<ProductType> AddProductType(ProductType productType)
        {
            throw new NotImplementedException();
        }

        public Task<ProductBrand> DeleteBrand(ProductBrand productBrand)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductType> DeleteProductType(ProductType productType)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductBrand> GetBrand(int id) => await _storeContext.ProductBrands.FindAsync(id);
        
        public async Task<IReadOnlyList<ProductBrand>> GetBrands() => await _storeContext.ProductBrands.ToListAsync();
        
        public async Task<Product> GetProductById(int id)
        {
            var product = await _storeContext.Products.FindAsync(id);
            return product;
        }

        public IReadOnlyList<Product> GetProducts(Func<Product, bool> filter)
        {
            var products = _storeContext.Products
                .Where(filter == null ? p => true: filter)
                .ToList<Product>();
            return products;
        }

        public async Task<ProductType> GetProductType(int id) => await _storeContext.ProductTypes.FindAsync(id);

        public async Task<IReadOnlyList<ProductType>> GetProductTypes() => await _storeContext.ProductTypes.ToListAsync();
        
        public Task<ProductBrand> UpdateBrand(ProductBrand productBrand)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<ProductType> UpdateProductType(ProductType productType)
        {
            throw new NotImplementedException();
        }
    }
}
