using FOMessenger.Code.User;
using MySql.Data.MySqlClient;

namespace FOMessenger.Code.Storage.Database
{
    public partial class DatabaseManager
    {
        public bool DoesUserExist(string username)
        {
            Global.Logger.LogInformation($"Checking if user:\"{username}\" exists...");

            string commandUsers = $"SELECT COUNT(1) FROM `users` WHERE `Username` LIKE '{username}';";
            MySqlCommand cmdUsers = new MySqlCommand(commandUsers, DatabaseConnection);
            MySqlDataReader readerUsers = cmdUsers.ExecuteReader();
            readerUsers.Read();

            string commandUserActivities = $"SELECT COUNT(1) FROM `users` WHERE `Username` LIKE '{username}';";
            MySqlCommand cmUserActivities = new MySqlCommand(commandUserActivities, DatabaseConnection);
            MySqlDataReader readerUserActivities = cmUserActivities.ExecuteReader();
            readerUserActivities.Read();

            while (readerUserActivities.Read() && readerUsers.Read())
            {
                Global.Logger.LogInformation($"Checked if user:\"{username}\" exists.");

                // todo - Add logic to fix database if only one of the tables includes the user.

                return readerUsers.GetBoolean(0) && readerUserActivities.GetBoolean(0);
            }

            Global.Logger.LogError("DoesUserExists couldn't read anything.");
            return false;
        }

        private void InsertUserToUsers(User.User user)
        {
            string command = $"INSERT INTO `users` " +
                $"(`Username`, `Password`, `Email`, `FirstName`, `LastName`, `Birthdate`) " +
                $"VALUES ('{user.Username}', '{user.HashedPassword}', '{user.Email}'," +
                $" '{user.FirstName}', '{user.LastName}', '{user.Birthdate.ToString("yyyy-MM-dd")}');";

            MySqlCommand cmd = new MySqlCommand(command, DatabaseConnection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            Global.Logger.LogInformation($"Inserted user:\"{user.Username}\" into the `users` table.");
        }

        public void InsertUser(User.User user)
        {
            Global.Logger.LogInformation($"Inserting user with username: \"{user.Username}\"...");
            
            InsertUserToUsers(user);

            InsertUserToUserActivities(user);

            Global.Logger.LogInformation($"User inserted with username: \"{user.Username}\".");
        }

        public void DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        private void RetrieveUserFromUsers(string username, ref User.User user)
        {
            string command = $"SELECT * FROM `users` WHERE `Username` LIKE '{username}';";
            MySqlCommand cmd = new MySqlCommand(command, DatabaseConnection);
            MySqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                string password = reader.GetString(1);
                string email = reader.GetString(2);
                string firstName = reader.GetString(3);
                string lastName = reader.GetString(4);
                DateOnly birthdate = DateOnly.ParseExact(reader.GetDateTime(5).ToString("yyyy-MM-dd"), "yyyy-MM-dd");

                user = new User.User(username, password, email, firstName, lastName, birthdate);
            }

            reader.Close();
            reader.Dispose();
            cmd.Dispose();

            Global.Logger.LogInformation($"Retrieved user:\"{username}\" from the `users` table.");
        }

        public User.User RetrieveUser(string username)
        {
            Global.Logger.LogInformation($"Retrieving user with username: \"{username}\"...");

            User.User user = new User.User();

            RetrieveUserFromUsers(username, ref user);

            RetrieveUserFromUserActivities(username, ref user);

            Global.Logger.LogInformation($"Retrieved user with username: \"{username}\".");

            return user;
        }
    }
}
