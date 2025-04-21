using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBlazorApp.DataServices;
using MyBlazorApp.Models;

namespace MyBlazorApp.Data
{
    public static class DbInitializer
    {
        public static void InitializeDatabase(MyDbContext context)
        {
//            context.Database.Migrate();

            // Add seed data here if necessary
            if (! context.Products.Any())
            {
 var pickleType = new ProductType { Name = "Pickle" };
 var preserveType = new ProductType { Name = "Preserves" };

 var dillReview = new Review
 {
     Date = DateTime.Now,
     Text = "These pickles pack a punch",
     UserId = "matt"
 };

 var dillPickles = new Product
 {
     Description = "Deliciously dill",
     Name = "Dill Pickles",
     ProductType = pickleType,
     Reviews = new List<Review> { dillReview }
 };

 var dillPickles2 = new Product
 {
     Description = "Deliciously fromage",
     Name = "Dill Pickles",
     ProductType = pickleType,
     Reviews = new List<Review> { }
 };

var beetReview = new Review
 {
     Date = DateTime.Now,
     Text = "Bonafide best beets",
     UserId = "matt"
 };

 var pickledBeet = new Product
 {
     Description = "unBeetable",
     Name = "Red Pickled Beets",
     ProductType = pickleType,
     Reviews = new List<Review> { beetReview }
 };

 var preserveReview = new Review
 {
     Date = DateTime.Now,
     Text = "Succulent strawberries making biscuits better",
     UserId = "matt"
 };

 var strawberryPreserves = new Product
 {
     Description = "Sweet and a treat to make your toast the most",
     Name = "Strawberry Preserves",
     ProductType = preserveType,
     Reviews = new List<Review> { preserveReview }
 };

 context.Products.Add(dillPickles);
 context.Products.Add(dillPickles2);
 context.Products.Add(pickledBeet);
 context.Products.Add(strawberryPreserves);

 context.SaveChanges();
            }
        }
    }
}