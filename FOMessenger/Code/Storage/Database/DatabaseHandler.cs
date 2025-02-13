using MySql.Data.MySqlClient;

namespace FOMessenger.Code.Storage.Database
{
    public partial class DatabaseHandler : IDisposable, IAsyncDisposable
    {
        public MySqlConnection DatabaseConnection { get; set; }

        public DatabaseHandler() 
        {

        }

        public void OpenConnection()
        {
            string connectionString = "Host=localhost; Username=root; Password=''; Database=fom";
            DatabaseConnection = new MySqlConnection(connectionString);
            DatabaseConnection.Open();
            Global.Logger.LogInformation("Connection to the database established.");
        }

        public void CloseConnection()
        {
            DatabaseConnection.Close();
            Global.Logger.LogInformation("Connection to the database closed.");
        }


        // Make sure the connection closes if the object is Disposed (manually or automatically)
        public void Dispose()
        {
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
