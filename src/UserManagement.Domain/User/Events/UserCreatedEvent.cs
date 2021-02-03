using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Domain.Abstractions;

namespace UserManagement.Domain.User.Events
{
    internal class UserCreatedEvent : IDomainEvent
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }

        public string City { get; set; }

        public string Password { get; set; }

        public DateTime OccurredOn => DateTime.UtcNow;

    }
}
