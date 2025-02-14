using FOMessenger.Code.User;
using MySql.Data.MySqlClient;

namespace FOMessenger.Code.Storage.Database
{
    public partial class DatabaseManager
    {
        private void InsertUserToUserActivities(User.User user)
        {
            string command = $"INSERT INTO `useractivities` (`Username`, `ActivityState`, `LastOnline`)" +
            $" VALUES ('{user.Username}', '{user.ActivityState}'," +
            $" '{user.LastOnline.ToString("yyyy-MM-dd HH:mm:ss")}');";
            
            MySqlCommand cmd = new MySqlCommand(command, DatabaseConnection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            Global.Logger.LogInformation($"Inserted user:{user.Username} into the `userActivities` table.");
        }
    }
}
