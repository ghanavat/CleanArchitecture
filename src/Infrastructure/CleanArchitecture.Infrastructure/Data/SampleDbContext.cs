using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CleanArchitecture.Infrastructure.Data;

/// <summary>
/// DB Context. All configurations related to the Entities are done here.
/// </summary>
#pragma warning disable CS1591
public sealed class SampleDbContext : DbContext
{
    public DbSet<Player> Players => Set<Player>();

    public SampleDbContext(DbContextOptions options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(p => p.Id).HasConversion(new CustomIdentityConverter());
            entity.Property(p => p.FirstName).HasElementName("firstName");
            entity.Property(p => p.LastName).HasElementName("lastName");

            entity.ToCollection("PlayTime");
        });

        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
