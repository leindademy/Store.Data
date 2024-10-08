using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.productBrands != null && !context.productBrands.Any())
                {
                    //Presist Data To DB
                    var brandsData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData); //Reverse 
                    if (brands is not null)
                    {
                        await context.productBrands.AddRangeAsync(brands);
                        await context.SaveChangesAsync();
                    }
                } 

                if (context.productTypes != null && !context.productTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Store.Repository/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                    if (Types is not null)
                    {
                        await context.productTypes.AddRangeAsync(Types);
                        await context.SaveChangesAsync();
                    }
                }

                if (context.Products != null && !context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Store.Repository/SeedData/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products is not null)
                    {
                        await context.Products.AddRangeAsync(Products);
                        await context.SaveChangesAsync();
                    }
                }

                if (context.DeliveryMethods!= null && !context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Store.Repository/SeedData/Delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod >>(deliveryMethodsData);

                    if (deliveryMethods is not null)
                    {
                        await context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                       
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception exe)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(exe.Message);
            }
        }
        
    }
}
