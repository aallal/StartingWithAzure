using Microsoft.EntityFrameworkCore;
using MyBlazorApp.DataServices;
using MyBlazorApp.Models;

namespace MyBlazorApp.Services;

public class ReviewService
{
    private readonly MyDbContext _context;

    public ReviewService(MyDbContext context)
    {
        _context = context;
    }

    public async Task AddReview(string reviewText, int productId)
    {
      try
        {
            // Check if the product exists
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null)
            {
                //throw new InvalidOperationException("Product not found");
                return; 
            }
            if (product.Reviews is null)
            {
                product.Reviews = new List<Review>();
            }

            if (string.IsNullOrWhiteSpace(reviewText))
            {
                throw new ArgumentException("Review text cannot be empty", nameof(reviewText));
            }
            Review review = new Review
            {
                Text = reviewText,
                UserId = "testUser",
                Date = DateTime.Now,
               // Product = product ?? throw new InvalidOperationException("Product not found"),
            };

            product.Reviews.Add(review);

            
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding review: {ex.Message}");
        }
       
    }

    public async Task<IEnumerable<Review>> GetReviewsforProduct(int productId)
    {
        return await _context.Reviews.AsNoTracking()
            .Where(r => r.Product.Id == productId).ToListAsync();
    }
}