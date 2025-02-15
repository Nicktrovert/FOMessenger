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

        private void RetrieveUserFromUserActivities(string username, ref User.User user)
        {
            string command = $"SELECT * FROM `useractivities` WHERE `Username` LIKE '{username}';";

            MySqlCommand cmd = new MySqlCommand(command, DatabaseConnection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                UserActivityState activityState = Enum.Parse<UserActivityState>(reader.GetString(1));
                DateTime lastOnline = reader.GetDateTime(2);

                user.ActivityState = activityState;
                user.LastOnline = lastOnline;
            }

            Global.Logger.LogInformation($"Retrieved user:\"{username}\" from the `userActivities` table.");
        }
    }
}
