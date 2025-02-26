var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/api/productlist", () =>
{
    return new[]
    {
        new { Id = 1, Name = "Laptop", Price = 1200.50, Stock = 25 ,
                    Category = new { Id = 101, Name = "Electronics" }

         },
        new { Id = 2, Name = "Headphones", Price = 50.00, Stock = 100 ,  Category = new { Id = 102, Name = "Accessories" } }
    };
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
