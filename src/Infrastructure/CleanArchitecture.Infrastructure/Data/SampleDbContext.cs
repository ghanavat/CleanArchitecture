using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Infrastructure.Helpers;
using CleanArchitecture.Shared;
using CleanArchitecture.Shared.DomainEventMechanism;
using CleanArchitecture.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;
using MongoDB.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Data;

/// <summary>
/// DB Context. All configurations related to the Entities are done here.
/// </summary>
public sealed class SampleDbContext : DbContext
{
    private readonly IDomainEventPublisher _eventPublisher;

    public DbSet<Player> Players => Set<Player>();

    public SampleDbContext(DbContextOptions options, IDomainEventPublisher eventPublisher) : base(options)
    {
        _eventPublisher = eventPublisher.CheckForNull();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new CamelCaseElementNameConvention());
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(p => p.Id).HasConversion(new CustomIdentityConverter());
            entity.Property(p => p.Id).HasBsonRepresentation(BsonType.ObjectId); // The above line is not needed. But test it first.

            entity.Property(p => p.FirstName).HasElementName("firstName");
            entity.Property(p => p.LastName).HasElementName("lastName");

            entity.ToCollection("PlayTime");
        });

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var events = ChangeTracker.Entries<EntityBase>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Count != 0);

        await _eventPublisher.PublishDomainEventsAsync(events, cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
