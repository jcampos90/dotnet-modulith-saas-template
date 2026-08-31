using MediatR;

namespace Billing.Application.Outbox;

public interface IOutboxPublisher
{
    Task AddAsync(INotification notification, CancellationToken ct = default);
}
