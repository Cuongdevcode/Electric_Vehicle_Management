using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;

namespace Product.Infrastructure.Repositories
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ProductDbContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Check if data already exists
            if (await context.Categories.AnyAsync())
            {
                return; // Database has been seeded
            }

            // Seed Categories
            var categories = new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Electric Cars",
                    Description = "All electric car models",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Charging Stations",
                    Description = "EV charging equipment and accessories",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Batteries",
                    Description = "Replacement batteries and power banks",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Accessories",
                    Description = "EV accessories and parts",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            // Seed Products
            var products = new List<Domain.Entities.Product>
            {
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Tesla Model 3",
                    Description = "Long-range electric sedan with autopilot",
                    Price = 45000,
                    StockQuantity = 50,
                    ImageUrl = "https://example.com/tesla-model-3.jpg",
                    CategoryId = categories[0].Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Tesla Model Y",
                    Description = "Electric SUV with spacious interior",
                    Price = 55000,
                    StockQuantity = 40,
                    ImageUrl = "https://example.com/tesla-model-y.jpg",
                    CategoryId = categories[0].Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Home Charging Station",
                    Description = "Level 2 EV charger for home installation",
                    Price = 599,
                    StockQuantity = 100,
                    ImageUrl = "https://example.com/home-charger.jpg",
                    CategoryId = categories[1].Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Portable EV Charger",
                    Description = "Portable Level 1/2 charger with adapters",
                    Price = 399,
                    StockQuantity = 150,
                    ImageUrl = "https://example.com/portable-charger.jpg",
                    CategoryId = categories[1].Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    Name = "High-Capacity Battery Pack",
                    Description = "Extended range battery replacement",
                    Price = 8500,
                    StockQuantity = 20,
                    ImageUrl = "https://example.com/battery-pack.jpg",
                    CategoryId = categories[2].Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    Name = "All-Weather Floor Mats",
                    Description = "Custom-fit floor mats for electric vehicles",
                    Price = 149,
                    StockQuantity = 200,
                    ImageUrl = "https://example.com/floor-mats.jpg",
                    CategoryId = categories[3].Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
