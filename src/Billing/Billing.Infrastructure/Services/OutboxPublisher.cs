using System.Text.Json;
using Billing.Application.Outbox;
using Billing.Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infrastructure.Services;

public class OutboxPublisher : IOutboxPublisher
{
    private readonly BillingDbContext _dbContext;

    public OutboxPublisher(BillingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(INotification notification, CancellationToken ct = default)
    {
        var message = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            EventType = notification.GetType().AssemblyQualifiedName!,
            Payload = JsonSerializer.Serialize(notification, notification.GetType()),
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.OutboxMessages.Add(message);
    }
}
