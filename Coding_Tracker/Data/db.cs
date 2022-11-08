using Microsoft.Data.Sqlite;
using Coding_Tracker;
using Serilog;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Coding_Tracker.Data;
internal class Db
{

    public Db( string connectionString )
    {
        ConnectionString = connectionString;
    }

    private string ConnectionString { get; }

    public void CreateDatabase()
    {
        using ( var connection = new SqliteConnection( ConnectionString ) )
        {
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
                Log.Information( "Database created" );

            }
            catch ( Exception e )
            {
                Log.Information( "Database not Created", e );
                Console.WriteLine( "Sorry, the database could not be created" );
            }
        }
    }

    public void Add(DateTime date,
                    DateTime startTime, 
                    DateTime finishTime 
                   )
    {
        TimeSpan duration = finishTime - startTime;

        using var connection = new SqliteConnection(ConnectionString);

        using var command = connection.CreateCommand();

        
        connection.Open();

        command.CommandText = $@"INSERT INTO CodingTracker(date, startTime, finishTime, duration) VALUES (@date, @startTime, @finishTime, @duration)";

        command.Parameters.AddWithValue( "@date", date );
        command.Parameters.AddWithValue( "@startTime", startTime );
        command.Parameters.AddWithValue( "@finishTime", finishTime );
        command.Parameters.AddWithValue( "@duration", duration );

        try
        {
            command.ExecuteNonQuery();
            Log.Information( "Data Inserted Successfully" );
        }
        catch ( Exception e)
        {
            Log.Warning( "Data not Inserted" );
            Log.Debug( e.Message );
        }
    }
}

