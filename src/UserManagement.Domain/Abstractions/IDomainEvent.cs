using System;

namespace UserManagement.Domain.Abstractions
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
    }
}