using FOMessenger.Code.User;
using MySql.Data.MySqlClient;

namespace FOMessenger.Code.Storage.Database
{
    public partial class DatabaseHandler
    {
        public bool DoesUserExist(string username)
        {
            throw new NotImplementedException();

            return false;
        }

        public void InsertUser(User.User user)
        {
            string command = $"INSERT INTO `users` " +
                $"(`Username`, `Password`, `Email`, `FirstName`, `LastName`, `Birthdate`) " +
                $"VALUES ('{user.Username}', '{user.HashedPassword}', '{user.Email}'," +
                $" '{user.FirstName}', '{user.LastName}', '{user.Birthdate.ToString("yyyy-MM-dd")}');";

            MySqlCommand cmd = new MySqlCommand(command, DatabaseConnection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
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
