using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Helpers;
using CleanArchitecture.Shared.Attributes;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;

namespace CleanArchitecture.Infrastructure.Data;

/// <summary>
/// DB Context. All configurations related to the Entities done here.
/// </summary>
public class SampleDbContext : DbContext
{
    public DbSet<SampleEntity> UserPolicies => Set<SampleEntity>();

    public SampleDbContext(DbContextOptions options) : base(options)
    {
        options.ContextType.ToBson();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.Entity<SampleEntity>(entity =>
        {
            entity.Property(p => p.Id).HasConversion(new CustomObjectIdConverter());
            entity.Property(p => p.FirstName).HasElementName("firstName");

            entity.ToCollection("PlayTime");
        });

        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
