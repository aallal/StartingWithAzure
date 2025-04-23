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
            if (!context.Products.Any())
            {
                var CornichonType = new ProductType { Name = "Cornichon" };
                var ConservesType = new ProductType { Name = "Conserves" };

                var CornichonReview = new Review
                {
                    Date = DateTime.Now,
                    Text = "Ces cornichons ont du punch",
                    UserId = "Amen"
                };

                var Cornichons = new Product
                {
                    Description = "Délicieusement à l'aneth",
                    Name = "Cornichons à l'aneth",
                    ProductType = CornichonType,
                    Reviews = new List<Review> { CornichonReview }
                };


                var betteravesReview = new Review
                {
                    Date = DateTime.Now,
                    Text = "Les meilleures betteraves",
                    UserId = "Fatima"
                };

                var betteraves = new Product
                {
                    Description = "unBeetable",
                    Name = "Betteraves rouges marinées",
                    ProductType = CornichonType,
                    Reviews = new List<Review> { betteravesReview }
                };

                var fraisesReview = new Review
                {
                    Date = DateTime.Now,
                    Text = "Des fraises succulentes qui améliorent les biscuits",
                    UserId = "Iyad"
                };

                var strawberryPreserves = new Product
                {
                    Description = "Douceur et gourmandise pour que vos tartines soient les plus réussies",
                    Name = "Strawberry Preserves",
                    ProductType = ConservesType,
                    Reviews = new List<Review> { fraisesReview }
                };

                context.Products.Add(Cornichons);
                context.Products.Add(betteraves);
                context.Products.Add(strawberryPreserves);

                context.SaveChanges();
            }
        }
    }
}