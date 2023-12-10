using CalendarioVale.Test.Data;
using Microsoft.EntityFrameworkCore;

namespace CalendarioVale.Test;

[Collection("Database")]
public class DbCollectionTest : DatabaseTestCase
{
    public DbCollectionTest() : base()
    {
    }

    // [Fact]
    // public async void TemporaryDBShouldBeCreated()
    // {
    //     Assert.True(DbContext.Database.CanConnect());
    //     Assert.Contains("192.168.14.108", DbContext.Database.GetConnectionString());
    // }
}