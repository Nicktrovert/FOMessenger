using FOMessenger.Code.User;
using MySql.Data.MySqlClient;

namespace FOMessenger.Code.Storage.Database
{
    public partial class DatabaseManager
    {
        public bool DoesUserExist(string username)
        {
            throw new NotImplementedException();

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

            Global.Logger.LogInformation($"Inserted user:{user.Username} into the `users` table.");
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

        public void RetrieveUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
