using MySql.Data.MySqlClient;

namespace FOMessenger.Code.Storage.Database
{
    public partial class DatabaseManager : IDisposable, IAsyncDisposable
    {
        private const string HOST = "192.168.178.37";
        private const string USERNAME = "admin";
        private const string PASSWORD = "iq0t]DlmOhQKhWRw";
        private const string DATABASE = "fom";

        public MySqlConnection? DatabaseConnection { get; set; }

        public DatabaseManager() { ; }

        public void OpenConnection()
        {
            if (DatabaseConnection == null)
            {
                string connectionString = $"Host={HOST}; Username={USERNAME}; Password='{PASSWORD}'; Database={DATABASE}";
                DatabaseConnection = new MySqlConnection(connectionString);
                DatabaseConnection.Open();
                Global.Logger.LogInformation("Connection to the database established and opened.");
            }
            else if (DatabaseConnection.State != System.Data.ConnectionState.Open)
            {
                Global.Logger.LogInformation("Connection to the database is established but not open.");
                DatabaseConnection.Close();
                DatabaseConnection.Open();
                Global.Logger.LogInformation("Connection to the database was reopened.");
            }
            else
            {
                Global.Logger.LogInformation("Connection to the database already established");
            }
        }

        public void CloseConnection()
        {
            if (DatabaseConnection != null)
            {
                DatabaseConnection.Close();
                Global.Logger.LogInformation("Connection to the database closed.");
            } 
            else
            {
                Global.Logger.LogInformation("Connection to the database is already closed.");
            }
        }


        // Make sure the connection closes if the object is Disposed (manually or automatically)
        public void Dispose()
        {
            if (DatabaseConnection == null) return;

            if (DatabaseConnection.State == System.Data.ConnectionState.Open) 
            {
                DatabaseConnection.Close();
            }
        }

        public ValueTask DisposeAsync()
        {
            Task task = new Task(Dispose);
            ValueTask valueTask = new ValueTask(task);
            valueTask.AsTask().Start();
            valueTask.AsTask().Wait();
            return valueTask;
        }
    }
}
