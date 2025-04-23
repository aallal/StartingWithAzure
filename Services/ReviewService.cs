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

    public async Task AddReview(string reviewText, List<string> photoUrls, int productId)
    {
        try
        {
            // create all the photo url object
            List<ReviewPhoto> photos = new List<ReviewPhoto>();
			
			foreach (var photoUrl in photoUrls)
			{
				photos.Add(new ReviewPhoto {  PhotoUrl = photoUrl });
			}
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
                UserId = "amen",
                Date = DateTime.Now,
                Photos = photos
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
    
    public async Task<Review?> GetReviewById(int reviewId)
    {
        return await _context
            .Reviews
            .Include(r => r.Product)
            .Include(r => r.Photos)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == reviewId)!;
    }
}