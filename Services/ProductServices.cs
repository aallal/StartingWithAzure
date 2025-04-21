using Microsoft.EntityFrameworkCore;
using MyBlazorApp.DataServices;
using MyBlazorApp.Models;

namespace MyBlazorApp.Services;

public class ProductServices
{
    private readonly MyDbContext _context;

    public ProductServices(MyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products
            .Include(p => p.ProductType)
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Where(p => p.Id == id).AsNoTracking().FirstOrDefaultAsync();
    }
}