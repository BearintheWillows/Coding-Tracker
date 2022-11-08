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


}

