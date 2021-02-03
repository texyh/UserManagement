using AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.UseCases.UserRegistration;
using UserManagement.Domain.Abstractions;
using UserManagement.Domain.User;
using Xbehave;
using Xunit;

namespace UserManagement.IntegrationTests.AcceptanceTests
{
    public class UserRegistrationScenerio : IClassFixture<TestServerFixture>
    {
        private TestServerFixture _fixture;

        public UserRegistrationScenerio(TestServerFixture fixture)
        {
            fixture.CreateTestEnvironment(services =>
            {
            });

            _fixture = fixture;
        }

        [Scenario]
        public void User_Registration(UserRegistrationRequest request, UserRegistrationResponse response)
        {
            "Given a user registraion request"
                .x(request => GivenUserRequest());

            "When i make a user registration request"
                .x(response => MakeUserRegistrationRequest(request));

            "Then ensure that the user is persisted"
                .x(() => AssertNewUserIsPersisted(response));

            "And userRegistered Event is published"
                .x(() => AssertUserRegisteredEventWasPublished());
        }

        private void AssertUserRegisteredEventWasPublished()
        {
            throw new NotImplementedException();
        }

        private void AssertNewUserIsPersisted(UserRegistrationResponse response)
        {
            var repository = _fixture.GetService<IAggregateStore<User>>();
            var user = repository.Load(response.Id);

            Assert.NotNull(user);
        }

        private async Task<UserRegistrationResponse> MakeUserRegistrationRequest(UserRegistrationRequest request)
        {
            var cancellation = new CancellationTokenSource();
            cancellation.CancelAfter(TimeSpan.FromSeconds(30));
            return await _fixture.PostAsync<UserRegistrationResponse, UserRegistrationRequest>("api/user/register", request, cancellation.Token);
        }

        private UserRegistrationRequest GivenUserRequest()
        {
            var fixture = new Fixture();
            return fixture.Create<UserRegistrationRequest>();
        }
    }
}
