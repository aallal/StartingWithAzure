using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using MyBlazorApp.Components;
using MyBlazorApp.Data;
using MyBlazorApp.DataServices;
using MyBlazorApp.Services;


var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<MyDbContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("MyConnection")));

// Ajout explicite des fichiers de configuration
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    // .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
     ;

// var connString = builder.Configuration.GetConnectionString("MyConnection");
// builder.Services.AddSqlite<MyDbContext>(connString);

var sqlConnection = builder.Configuration["ConnectionStrings:Amen:SqlDb"];
var storageConnection = builder.Configuration["ConnectionStrings:Amen:Storage"];

builder.Services.AddSqlServer<MyDbContext>(sqlConnection, options => options.EnableRetryOnFailure());
builder.Services.AddAzureClients(
    azureBuilder => azureBuilder
        .AddBlobServiceClient(storageConnection)
);

Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
Console.WriteLine($"Connection String: {sqlConnection}");

builder.Services.AddTransient<ProductServices>();
builder.Services.AddTransient<ReviewService>();

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.CreateDbIfNotExists();

app.Run();
