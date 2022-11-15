using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
