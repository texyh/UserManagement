using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Abstractions.Commands;
using UserManagement.Domain.Shared.Abstractions;
using UserManagement.Domain.User;

namespace UserManagement.Application.UseCases.UserRegistration
{
    public class UserRegistrationCommandHandler : 
        ICommandHandler<UserRegistrationCommand, UserRegistrationResponse>
    {
        private readonly IAggregateStore<User> _aggregateStore;

        public UserRegistrationCommandHandler(IAggregateStore<User> aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public async Task<UserRegistrationResponse> Handle(
            UserRegistrationCommand command,
            CancellationToken cancellationToken)
        {
            var user = new User(Guid.NewGuid(),
                                command.FirstName,
                                command.LastName,
                                command.Age,
                                command.EmailAddress,
                                command.MobileNumber,
                                command.City,
                                command.Password);

            await _aggregateStore.AppendChanges(user);
            return new UserRegistrationResponse { Id = user.Id };
        }
    }
}