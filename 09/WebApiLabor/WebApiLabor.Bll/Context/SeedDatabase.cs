using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiLabor.Bll.Entities;

namespace WebApiLabor.Bll.Context
{
    public static class SeedDatabase
    {
        public static void Seed(this NorthwindContext context)
        {
            if (!context.Products.Any())
            {
                var cat_drink = new Category() { Name = "Ital" };
                var cat_food = new Category() { Name = "Étel" };
                context.Categories.Add(cat_drink);
                context.Categories.Add(cat_food);
                context.Products.Add(new Product()
                {
                    Name = "Sör",
                    UnitPrice = 50,
                    Category = cat_drink,
                    ProductOrders = { new ProductOrder() { Order = new Order() { OrderDate = DateTime.Now } } }
                });
                context.Products.Add(new Product() { Name = "Bor", Category = cat_drink });
                context.Products.Add(new Product() { Name = "Tej", CategoryId = cat_drink.Id });
                context.SaveChanges();
            }
        }
    }
}
