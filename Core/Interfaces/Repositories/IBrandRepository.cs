using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        Task<ProductBrand> AddProductBrandAsync(ProductBrand productBrand);
        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        Task<ProductBrand> GetBrandAsync(int id);
        void DeleteBrand(ProductBrand productBrand);
        void UpdateBrand(ProductBrand productBrand);
    }
}
