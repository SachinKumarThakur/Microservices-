using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> product)
        {
            bool existProduct = product.Find(p => true).Any();
            if (!existProduct)
            {
                product.InsertManyAsync(GetPreConfigureProduct());
            }
        }

        private static IEnumerable<Product> GetPreConfigureProduct()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name="IPhone X",
                    Price=989811.99M,
                    ImageFile="product-1.png",
                    Category="Smart Phone",
                    Summary="This is latest advance Phone",
                    Description="Here is full Description of this Phone."
                },
                 new Product()
                {
                    Name="Samsung 10",
                    Price=878787,
                    ImageFile="product-2.png",
                    Category="Smart Phone",
                    Summary="This is latest advance Phone for INdia",
                    Description="Here is full Description of this Phone."
                }
            };
        }
    }
}
