using CalendarioVale.Data;
using Microsoft.EntityFrameworkCore;

namespace CalendarioVale.Test.Data
{
    public class DatabaseTestCase : IDisposable
    {
        public ApplicationDbContext DbContext { get; }

        // Constructor is called before every test
        public DatabaseTestCase()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql($"Host=localhost;Database=my_db_test_{Guid.NewGuid().ToString().Replace("-", "")};User ID=postgres;password=postgres");

            // Create an instance of your application's DbContext
            DbContext = new ApplicationDbContext(optionsBuilder.Options);
            DbContext.Database.Migrate();
            DbContext.Database.EnsureCreated();
        }

        // Dispose is called after every test
        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
        }
    }
}