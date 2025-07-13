using System.Linq.Expressions;
using LibraryManagementAPI.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LibraryManagementAPI.Infrastructure.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.UpdatedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTime.Now;
                    break;
            }

            // Capitalize all string properties
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.ClrType == typeof(string) && property.CurrentValue is string currentValue &&
                    !string.IsNullOrWhiteSpace(currentValue))
                {
                    property.CurrentValue = currentValue.ToUpperInvariant();
                }
            }
        }

        foreach (var entity in ChangeTracker.Entries<BaseModel>())
        {
            if (entity.State == EntityState.Modified)
            {
                var audit = new AuditLog();
                audit.TableName = entity.Entity.GetType().Name;
                audit.InitiatedDate = DateTime.Now;
                audit.Action = EntityState.Modified.ToString();
                audit.RecordId = entity.Property("Id").CurrentValue?.ToString();

                foreach (var property in entity.OriginalValues.Properties)
                {
                    var original = entity.OriginalValues[property];
                    var current = entity.CurrentValues[property];

                    if (!object.Equals(original, current))
                    {
                        audit.AffectedColumns += $"{property.Name}, ";
                        audit.OldValue += $"{original}, ";
                        audit.NewValue += $"{current}, ";
                    }
                }

                // Remove trailing commas
                audit.AffectedColumns = audit.AffectedColumns?.TrimEnd(',', ' ');
                audit.OldValue = audit.OldValue?.TrimEnd(',', ' ');
                audit.NewValue = audit.NewValue?.TrimEnd(',', ' ');
                AuditLogs.Add(audit);
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            //If the actual entity is an auditable type. 
            if (typeof(BaseModel).IsAssignableFrom(entityType.ClrType))
            {
                //add Global Query Filter to exclude deleted items
                //https://docs.microsoft.com/en-us/ef/core/querying/filters
                //That always excludes deleted items. Opt out by using dbSet.IgnoreQueryFilters()
                var parameter = Expression.Parameter(entityType.ClrType, "p");
                var deletedCheck =
                    Expression.Lambda(
                        Expression.Equal(Expression.Property(parameter, "IsDeleted"), Expression.Constant(false)),
                        parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
            }
        }
    }
}



