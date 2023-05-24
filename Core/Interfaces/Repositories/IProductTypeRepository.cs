using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IProductTypeRepository
    {
        Task<ProductType> AddProductTypeAsync(ProductType productType);
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<ProductType> GetProductTypeAsync(int id);
        void DeleteProductType(ProductType productType);
        void UpdateProductType(ProductType productType);
        public Task<bool> AnyAsync();
    }
}
