using Mehbang.WebAPI.Data.Entities.Students;
using Mehbang.WebAPI.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Mehbang.WebAPI.Infrastructure.Context;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(StudnetMapping).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    } 
}
