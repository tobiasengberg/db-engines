using Microsoft.EntityFrameworkCore;

namespace postgresql.data;

public class AreaDbContext : DbContext
{
    public AreaDbContext(DbContextOptions<AreaDbContext> options) : base(options)
    {
        
    }

    public DbSet<Location> Locations { get; set; }
    
}