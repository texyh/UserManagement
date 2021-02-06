using System;

namespace UserManagement.Domain.Shared.Abstractions
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
    }
}