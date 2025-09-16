using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvent(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        await DispatchDomainEvent(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvent(DbContext? context)
    {
        if (context is null) return;

        var aggregates = context.ChangeTracker.Entries<IAggregate>()
            .Where(aggregate => aggregate.Entity.DomainEvents.Any())
            .Select(aggregate => aggregate.Entity)
            .ToList();

        var domainEvents = aggregates.SelectMany(aggregate => aggregate.DomainEvents).ToList();

        aggregates.ForEach(aggregate => aggregate.ClearDomainEvents());

        await Parallel.ForEachAsync(domainEvents,
            async (domainEvent, cancellationToken) => { await mediator.Publish(domainEvent, cancellationToken); });
    }
}