using System.Text.Json;
using Talabat.Domain.Entities;
using Talabat.Domain.Entities.Order;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        //

        public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.ProductBrands.Any()) {
                #region Seeding the Brands
                //Seeding the Brands
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count > 0)
                {

                    foreach (var brand in Brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(brand);
                    }
                    await dbContext.SaveChangesAsync();
                }
                #endregion
            }

            if (!dbContext.ProductTypes.Any()) {
                #region Seeding the Types

                //Seeding the Types
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);


                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(type);
                    }
                    await dbContext.SaveChangesAsync();


                }

                #endregion
            }


            if (!dbContext.Products.Any())
            {
                #region Seeding the Products
                //seeding the products
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products?.Count > 0)
                {

                    foreach (var product in products)
                    {
                        await dbContext.Set<Product>().AddAsync(product);

                    }
                    await dbContext.SaveChangesAsync();


                }


                #endregion
            }
            //seeding delivery methods
            if (!dbContext.DeliveryMethods.Any())
            {
                #region Seeding the Delivery Methods
                var deliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                if (deliveryMethods?.Count > 0)
                {
                    foreach (var method in deliveryMethods)
                    {
                        await dbContext.Set<DeliveryMethod>().AddAsync(method);
                    }
                    await dbContext.SaveChangesAsync();
                }
                #endregion
            }
        }

    }
}
