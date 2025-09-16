using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateEntities(DbContext? context)
    {
        if (context is null) return;

        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = "sa";
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = "sa";
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified || entry.HasChangedOwnEntities())
            {
                entry.Entity.UpdatedBy = "sa";
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}

public static class EntityExtensions
{
    public static bool HasChangedOwnEntities(this EntityEntry entry)
    {
        return entry.References.Any(reference =>
            reference.TargetEntry != null &&
            reference.TargetEntry.Metadata.IsOwned() &&
            (reference.TargetEntry.State == EntityState.Added || reference.TargetEntry.State == EntityState.Modified));
    }
}