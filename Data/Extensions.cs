using MyBlazorApp.DataServices;

namespace MyBlazorApp.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var myDbContext = services.GetRequiredService<MyDbContext>();

        myDbContext.Database.EnsureCreated();

       DbInitializer.InitializeDatabase(myDbContext);        
    }
}
