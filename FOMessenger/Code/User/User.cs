namespace FOMessenger.Code.User
{
    public class User
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthdate { get; set; }

        public User() { }

        public User(string username, string hashedPassword, string email, string firstname, string lastname, DateOnly birthdate)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Email = email;
            FirstName = firstname; 
            LastName = lastname;
            Birthdate = birthdate;
        }
    }
}
