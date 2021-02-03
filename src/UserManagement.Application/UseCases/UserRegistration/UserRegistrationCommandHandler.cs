using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Abstractions.Commands;

namespace UserManagement.Application.UseCases.UserRegistration
{
    public class UserRegistrationCommandHandler : 
        ICommandHandler<UserRegistrationCommand, UserRegistrationResponse>
    {
        public Task<UserRegistrationResponse> Handle(
            UserRegistrationCommand request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(new UserRegistrationResponse());
        }
    }
}