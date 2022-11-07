using Microsoft.Data.Sqlite;

namespace Coding_Tracker.Data;
internal class db
{
    public static string ConnectionString
    {
        get
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Coding_Tracker.Properties.Settings.CodingTrackerConnectionString"].ConnectionString;
        }
    }

    public static void CreateDatabase()
    {
        using var connection = new SqliteConnection(ConnectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText = @"CREATE TABLE IF NOT EXISTS CodingTracker 
                                (Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                 Date TEXT, 
                                 StartTime TEXT, 
                                 FinishTime TEXT,
                                 Duration TEXT
)";

        try
        {
            command.ExecuteNonQuery();
        }
        catch ( Exception e )
        {
            Console.WriteLine.de ;
        }
    }
    }
}
