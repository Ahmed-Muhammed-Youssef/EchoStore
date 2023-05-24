using Core.Entities;
using Core.Interfaces;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seeding
{
    public class SeedStoreContext
    {
        public static async Task SeedAsync(IUnitOfWork unitOfWork)
        {
            // ProductBrands
            if (! await unitOfWork.BrandRepository.AnyAsync())
            {
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Exxon" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Kodak" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Verizon" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Adidas" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Google" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Pixar" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Samsung" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Apple" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Microsoft" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Walmart" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "PlayStation" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Starbucks" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Logiteck" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "PUMA" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Cottonil" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Tornado" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Venus" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Chipsy" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Elsewedy Electric" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "Zara" });
                await unitOfWork.BrandRepository.AddProductBrandAsync(new ProductBrand() { Name = "LG" });
                await unitOfWork.Complete();
            }
            // ProductTypes
            if (!await unitOfWork.ProductTypeRepository.AnyAsync())
            {
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Hats" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Boots" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Gloves" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Mobile Phone" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Tablets" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Mobile Accessories" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Video Games" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Toys" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Furniture" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "TVs" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Books" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Smart Watches" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Watches" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Food" });
                await unitOfWork.ProductTypeRepository.AddProductTypeAsync(new ProductType() { Name = "Bags" });
                await unitOfWork.Complete();
            }
        }
    }
}
