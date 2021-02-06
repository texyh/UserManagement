namespace UserManagement.Application.UseCases.UserRegistration
{
    public class UserRegistrationRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }

        public string City { get; set; }

        public string Password { get; set; }

        public UserRegistrationCommand ToCommand()
        {
            return new UserRegistrationCommand
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                MobileNumber = MobileNumber,
                City = City,
                Password = Password,
                Age = Age
            };
        }
    }
}
