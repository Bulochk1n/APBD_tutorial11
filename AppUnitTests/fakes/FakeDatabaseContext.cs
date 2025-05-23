
using APBD_tutorial11.Data;
using Microsoft.EntityFrameworkCore;

public static class FakeDatabaseContext
{
    public static DatabaseContext GetFakeContext(string dbName = "TestDb")
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new DatabaseContext(options);

        context.Database.EnsureDeleted(); // чистим каждый раз
        context.Database.EnsureCreated(); // пересоздаём

        return context;
    }
}
