using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // Add memory cache services
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/api/productlist", (IMemoryCache cache) =>
{
    const string cacheKey = "productList";
    if (!cache.TryGetValue(cacheKey, out var products))
    {
        products = new[]
        {
            new { Id = 1, Name = "Laptop", Price = 1200.50, Stock = 25 ,
                        Category = new { Id = 101, Name = "Electronics" }

             },
            new { Id = 2, Name = "Headphones", Price = 50.00, Stock = 100 ,  Category = new { Id = 102, Name = "Accessories" } }
        };

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // Set cache expiration

        cache.Set(cacheKey, products, cacheEntryOptions);
    }

    return products;
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.MapControllers();

app.Run();