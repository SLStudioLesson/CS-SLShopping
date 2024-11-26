using Microsoft.EntityFrameworkCore;
using SLShopping.Data;
using SLShopping.Models;

namespace SLShopping.Tests;

public class DatabaseFixture
{
    public ApplicationDbContext Context { get; private set; }

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SLShoppingTestDB")
            .Options;

        Context = new ApplicationDbContext(options);

        var data = new List<Brand>
        {
            new Brand { Id = 1, Name = "Ephemeral Bloom", Color = "Red" },
            new Brand { Id = 2, Name = "Urban Nomad", Color = "Green" },
            new Brand { Id = 3, Name = "Noir Élégance", Color = "Gold" },
            new Brand { Id = 4, Name = "Luna Veil" , Color = "Yellow"},
            new Brand { Id = 5, Name = "Echo Atelier" , Color = "Blue"},
            new Brand { Id = 6, Name = "Nova Fusion" , Color = "Silver"},
            new Brand { Id = 7, Name = "Zen Mode" , Color = "Black"},
            new Brand { Id = 8, Name = "Urban Mirage" , Color = "White"},
            new Brand { Id = 9, Name = "Chronos Silhouette" , Color = "Gray"},
            new Brand { Id = 10, Name = "Sonora Dream" , Color = "Blue"},
        }.AsQueryable();

        Context.Brands.AddRange(data);
        Context.SaveChanges();
    }
}