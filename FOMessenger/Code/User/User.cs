namespace FOMessenger.Code.User
{
    public class User
    {
        public string Username;
        public string HashedPassword;
        public string Email;
        public string FirstName;
        public string LastName;
        public DateOnly Birthdate;

        public UserActivityState ActivityState 
        {
            get
            {
                // Do Database call to get ActivityState
                return UserActivityState.Offline;
            }
            set
            {
                // Do Database call to change ActivityState
                return;
            }
        }

        public DateTime LastOnline 
        { 
            get
            {
                // Do Database Call for LastOnline
                return DateTime.Now;
            }
            set
            {
                // Do Database call to change value
                value = DateTime.Now;
            }
        }

        public User() { }

        public User(string username, string hashedPassword, string email, string firstname, string lastname, DateOnly birthdate, UserActivityState activityState = UserActivityState.Offline)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Email = email;
            FirstName = firstname; 
            LastName = lastname;
            Birthdate = birthdate;
            ActivityState = activityState;
            LastOnline = DateTime.Now; //temp
        }


    }
}
