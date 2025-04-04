using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class AppDbContextFactory
{

    public static AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\ECutbildningen\\DatabasLOCAL\\AlphaDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        return new AppDbContext(optionsBuilder.Options);
    }
}
