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

        public string PassWord { get; set; }
    }
}
