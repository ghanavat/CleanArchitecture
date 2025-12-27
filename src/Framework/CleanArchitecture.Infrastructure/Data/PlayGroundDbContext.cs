using System.Reflection;
using CleanArchitecture.Core.GameAggregate;
using CleanArchitecture.Core.PlayerAggregate;
using CleanArchitecture.Shared.Extensions;
using Ghanavats.Domain.Primitives;
using Ghanavats.Domain.Primitives.DomainEventMechanism;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Data;

public class PlayGroundDbContext : DbContext
{
    private readonly IDomainEventPublisher _eventPublisher;
    
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Game> Games => Set<Game>();
    
    public PlayGroundDbContext(DbContextOptions<PlayGroundDbContext> options, IDomainEventPublisher eventPublisher) 
        : base(options)
    {
        _eventPublisher = eventPublisher.CheckForNull();
    }
    
    // protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    // {
    //     //configurationBuilder.Conventions.Add(_ => new CamelCaseElementNameConvention());
    //     base.ConfigureConventions(configurationBuilder);
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.Entity<Player>(entity =>
        {
            entity.Property(x => x.Id).HasColumnName("PlayerId");
            
            entity.HasKey(x => x.Id).HasName("PK_Players_PlayerId");
            entity.Property(x => x.FirstName).IsRequired();
            entity.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        });
        
        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(x => x.Id).HasColumnName("GameId");
            entity.HasKey(x => x.Id).HasName("PK_Games_GameId");
            
            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(25);
            
            entity.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(x => x.Comment)
                .HasMaxLength(100);
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
