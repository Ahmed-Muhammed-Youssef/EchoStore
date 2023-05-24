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
            // ProductInfo
            if (!await unitOfWork.ProductRepository.AnyProductInfoAsync())
            {
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo() { 
                    Name= "IPhone 12",
                    Description= "orem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1200,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl= "images/products/iphone12.png",
                    ProductTypeId = 4,
                    ProductBrandId = 8
                }); 
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo() { 
                    Name= "IPhone 11 White",
                    Description = "orem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 900,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/iphone11.png",
                    ProductTypeId = 4,
                    ProductBrandId = 8
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "Samsung Galaxy S20",
                    Description = "orem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 900,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/s20.png",
                    ProductTypeId = 4,
                    ProductBrandId = 7
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "Samsung Galaxy Tab S6",
                    Description = "lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 900,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/tabs6.png",
                    ProductTypeId = 5,
                    ProductBrandId = 7
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "LG OLED TV 65 Inch A1 Series, Cinema Screen Design 4K Cinema HDR WebOS Smart AI ThinQ Pixel Dimming OLED65A1PVA (2021)",
                    Description = "LG 4K SELF-LIT OLED for extraordinary detail and contrast.\r\nPerfect Color ( 100% color fidelity ) Eye Comfort Display.\r\nα7 Gen4 AI Processor 4k with AI 4K upscaling ,AI Picture.\r\nThinQ AI webOS Smart TV with Magic Remote.\r\nTrue Cinema Experience suported with Dolby Vision IQ | Dolby Atmos.\r\nUnlimited Gameplay with , HGiG , ALLM , HDMI 2.0 eARC",
                    Price = 2000,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/tabs6.png",
                    ProductTypeId = 10,
                    ProductBrandId = 21
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "Winter Face Mask Hood Helmet Protection Balaclava Hat Headgear, game, outdoor wind masked.",
                    Description = "Color: Black. (As per picture)\r\nGender: Unisex\r\nMaterial: Cotton.\r\nSize: Head circumference :56 ~ 60cm. (Refer pictures)\r\nItem type: Mask, Face cover, Hat, Dust cover, Scarf.\r\nPackage includes: 1 psc \r\nMULTIPURPOSE: Protect your necks, ears, nose and head tremendously from the cold wind. Can be used as scarf, hat and dust cover.\r\nADJUSTABLE: Cinched hood adjusts coverage, one size fits the most, suitable for both men, women and children.\r\nKEEP YOU WARM: Easy to wear. Perfect for walking the dog, fishing, camping, hiking, snowboarding, skiing, motorcycle/bicycle riding and other outdoor activities in the winter.\r\nHELPS KEEP YOU WARM DURING COLD WEATHER - A full face cover balaclava hood style mask can be worn to help your entire face stay warm when you need it most.",
                    Price = 20,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/facemask.png",
                    ProductTypeId = 1,
                    ProductBrandId = 21
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "for iPhone 11 Case,Ultra-Thin HD Clear Slim Soft TPU Protective Case, Hard PC Back + Soft TPU Frame Shock-Absorption Anti-Scratch Cover Cases for iPhone 11 (6.1 inch)",
                    Description = "lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 900,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/iphone11case.png",
                    ProductTypeId = 6,
                    ProductBrandId = 8
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "Samsung Galaxy A23 Dual SIM BLACK 4GB RAM 128GB 4G",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 300,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/samsungalaxyA23.png",
                    ProductTypeId = 4,
                    ProductBrandId = 7
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "New Apple iPhone 14 Pro Max (256 GB) - Deep Purple",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 2000,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/iphone14.png",
                    ProductTypeId = 4,
                    ProductBrandId = 8
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "New Apple iPhone 14 Pro Max (256 GB) - Black",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 2000,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/iphone14.png",
                    ProductTypeId = 4,
                    ProductBrandId = 8
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "New Apple iPhone 14 Pro Max (256 GB) - Deep Blue",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 2000,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/iphone14.png",
                    ProductTypeId = 4,
                    ProductBrandId = 8
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "Apple iPhone 11 with FaceTime - 128GB, 4GB RAM, 4G LTE, Black, Single SIM & E-SIM",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 900,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/iphone11.png",
                    ProductTypeId = 4,
                    ProductBrandId = 8
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "Apple iPhone 13 Pro Max 256GB Blue",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1000,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/iphone13procase.png",
                    ProductTypeId = 4,
                    ProductBrandId = 8
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "Apple iPhone 13 Pro Max 256GB Black",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1000,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/iphone13procase.png",
                    ProductTypeId = 4,
                    ProductBrandId = 8
                });
                await unitOfWork.ProductRepository.AddProductInfoAsync(new ProductInfo()
                {
                    Name = "Apple iPhone 13 Pro Max 256GB White",
                    Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1000,
                    AvailableAmount = 0,
                    Rate = 0,
                    PictureUrl = "images/products/iphone13procase.png",
                    ProductTypeId = 4,
                    ProductBrandId = 8
                });
                await unitOfWork.Complete();
            }
            // Products
            if(!await unitOfWork.ProductRepository.AnyProductAsync())
            {
                for (int i = 0; i < 20; i++)
                {
                    await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 1 });
                }
                for (int i = 0; i < 7; i++)
                {
                    await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 2 });
                }
                await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 3 });
                for (int i = 0; i < 10; i++)
                {
                    await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 4 });
                }
                for (int i = 0; i < 3; i++)
                {
                    await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 5 });
                }
                for (int i = 0; i < 3; i++)
                {
                    await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 6 });
                }
                await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 7 });
                for (int i = 0; i < 50; i++)
                {
                    await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 8 });
                }
                await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 9 });
                await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 9 });
                for (int i = 0; i < 10; i++)
                {
                    await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 10 });
                }
                await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 11 });
                await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 12 });
                await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 12 });
                for (int i = 0; i < 4; i++)
                {
                    await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 13 });
                }
                await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 14 });
                for (int i = 0; i < 7; i++)
                {
                    await unitOfWork.ProductRepository.AddProductAsync(new Product() { ProductInfoId = 15 });
                }
                await unitOfWork.Complete();
            }
        }
    }
}
