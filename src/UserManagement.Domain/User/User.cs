using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Domain.Abstractions;
using UserManagement.Domain.User.Events;
using Volo.Abp;

namespace UserManagement.Domain.User
{
    public class User : AggregateRoot
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int Age { get; private set; }

        public string EmailAddress { get; private set; }

        public string MobileNumber { get; private set; }

        public string City { get; private set; }

        public string Password { get; private set; }

        private User()
        {

        }

        public User(
            Guid id,
            string firstName,
            string lastName,
            int age,
            string email,
            string mobileNumber,
            string city,
            string password)
        {
            Check.NotNull<Guid>(id, nameof(id));
            Check.NotNullOrEmpty(firstName, nameof(firstName));
            Check.NotNullOrEmpty(lastName, nameof(lastName));
            Check.NotNull(age, nameof(age));
            Check.NotNullOrEmpty(email, nameof(email));
            Check.NotNullOrEmpty(mobileNumber, nameof(mobileNumber));
            Check.NotNullOrEmpty(city, nameof(city));
            Check.NotNullOrEmpty(password, nameof(password));

            var @event = new UserCreatedEvent
            {
                Id = id,
                FirstName = firstName,
                Age = age,
                City = city,
                EmailAddress = email,
                LastName = lastName,
                MobileNumber = mobileNumber,
                Password = password
            };

            AddDomainEvent(@event);
            Apply(@event);
        }

        private void Apply(UserCreatedEvent @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            Age = @event.Age;
            Id = @event.Id;
            City = @event.City;
            EmailAddress = @event.EmailAddress;
            MobileNumber = @event.MobileNumber;
            Password = @event.Password;
        }
    }
}
