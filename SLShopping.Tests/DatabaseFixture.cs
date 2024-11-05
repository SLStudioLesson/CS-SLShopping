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
            new Brand { Id = 1, Name = "Ephemeral Bloom" },
            new Brand { Id = 2, Name = "Urban Nomad" },
            new Brand { Id = 3, Name = "Noir Élégance" },
            new Brand { Id = 4, Name = "Luna Veil" },
            new Brand { Id = 5, Name = "Echo Atelier" },
            new Brand { Id = 6, Name = "Nova Fusion" },
            new Brand { Id = 7, Name = "Zen Mode" },
            new Brand { Id = 8, Name = "Urban Mirage" },
            new Brand { Id = 9, Name = "Chronos Silhouette" },
            new Brand { Id = 10, Name = "Sonora Dream" },
        }.AsQueryable();

        Context.Brands.AddRange(data);
        Context.SaveChanges();
    }
}