using Microsoft.Data.Sqlite;
using Coding_Tracker;
using Serilog;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Coding_Tracker.Models;

namespace Coding_Tracker.Data;

public class Db
{
    public Db(string connectionString)
    {
        ConnectionString = connectionString;
    }

    private string ConnectionString { get; }

    public void CreateDatabase()
    {
        using var connection = new SqliteConnection(ConnectionString);
        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText =
            @"CREATE TABLE IF NOT EXISTS CodingTracker 
                                (Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                 Date TEXT, 
                                 StartTime TEXT, 
                                 FinishTime TEXT,
                                 Duration TEXT
                                 )";

        try
        {
            command.ExecuteNonQuery();
            Log.Information("Database created");
        }
        catch (Exception e)
        {
            Log.Information("Database not Created", e);
            Console.WriteLine("Sorry, the database could not be created");
        }
    }

    public void Add(DateTime date, TimeSpan startTime, TimeSpan finishTime)
    {
        TimeSpan duration = finishTime - startTime;

        using var connection = new SqliteConnection(ConnectionString);

        using var command = connection.CreateCommand();

        connection.Open();

        command.CommandText =
            "INSERT INTO CodingTracker(Date, StartTime, FinishTime, Duration) VALUES (@date, @startTime, @finishTime, @duration)";

        command.Parameters.AddWithValue("@date", date);
        command.Parameters.AddWithValue("@startTime", startTime);
        command.Parameters.AddWithValue("@finishTime", finishTime);
        command.Parameters.AddWithValue("@duration", duration);

        try
        {
            command.ExecuteNonQuery();
            Log.Information("Data Inserted Successfully");
        }
        catch (Exception e)
        {
            Log.Warning("Data not Inserted");
            Log.Debug(e.Message);
        }
    }

    public List<CodingSession> GetAll()
    {
        using var connection = new SqliteConnection(ConnectionString);
        using var command = connection.CreateCommand();

        // Open the connection
        connection.Open();

        // Select all rows from the table
        command.CommandText = "SELECT * FROM CodingTracker";

        //Create SQLiteDataReader object
        using var reader = command.ExecuteReader();

        //Create a list to store the results
        var codingSessions = new List<CodingSession>();

        //Read the data and store them in the list
        while (reader.Read())
        {
            var codingSession = new CodingSession
            {
                Id = reader.GetInt32(0),
                Date = reader.GetDateTime(1),
                StartTime = reader.GetTimeSpan(2),
                FinishTime = reader.GetTimeSpan(3),
                Duration = reader.GetTimeSpan(4)
            };

            codingSessions.Add(codingSession);
        }

        return codingSessions;
    }

    public CodingSession? GetById(int id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        using var command = connection.CreateCommand();

        // Open the connection
        connection.Open();

        // Select all rows from the table
        command.CommandText = "SELECT * FROM CodingTracker WHERE Id = @id";

        command.Parameters.AddWithValue("@id", id);
        //Create SQLiteDataReader object
        using var reader = command.ExecuteReader();

        //Create a list to store the results
        var codingSessions = new List<CodingSession>();

        //Check if reader has any rows
        //If true, read rows and return the first row
        //If false, return null and warning
        //Read the data and store them in the list

        if (reader.HasRows)
        {
            reader.Read();

            var codingSession = new CodingSession
            {
                Id = reader.GetInt32(0),
                Date = reader.GetDateTime(1),
                StartTime = reader.GetTimeSpan(2),
                FinishTime = reader.GetTimeSpan(3),
                Duration = reader.GetTimeSpan(4)
            };

            return codingSession;
        }
        else
        {
            Log.Warning("No Coding Session with that Id");
            return null;
        }
    }

    public void Update(CodingSession codingSession)
    {
        using var connection = new SqliteConnection(ConnectionString);
        using var command = connection.CreateCommand();
        // Open the connection
        connection.Open();

        //Select from the table where Id = @id
        command.CommandText = "SELECT * FROM CodingTracker WHERE Id = @id";
        command.Parameters.AddWithValue("@id", codingSession.Id);
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Close();

            command.Parameters.AddWithValue("@date", codingSession.Date);
            command.Parameters.AddWithValue("@startTime", codingSession.StartTime);
            command.Parameters.AddWithValue("@finishTime", codingSession.FinishTime);
            command.Parameters.AddWithValue("@duration", codingSession.Duration);
            // Select all rows from th table

            command.CommandText = "UPDATE CodingTracker SET Date = @date, StartTime = @startTime, FinishTime = @finishTime, Duration = @duration WHERE Id = @id";
            try
            {
                command.ExecuteNonQuery();
                Log.Information("Data Updated Successfully");
            }
            catch (Exception e)
            {
                Log.Warning("Data not Updated");
                Log.Debug(e.Message);
            }
        }
        else
        {
            Log.Warning("No Coding Session with that Id");
        }
    }

    public void DeleteById(int id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        using var command = connection.CreateCommand();
        // Open the connection
        connection.Open();

        //Select from the table where Id = @id
        command.CommandText = "SELECT * FROM CodingTracker WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Close();

            command.CommandText = "DELETE FROM CodingTracker WHERE Id = @id";
            try
            {
                command.ExecuteNonQuery();
                Log.Information("Data Deleted Successfully");
            }
            catch (Exception e)
            {
                Log.Warning("Data not Deleted");
                Log.Debug(e.Message);
            }
        }
        else
        {
            Log.Warning("No Coding Session with that Id");
        }
    }
}
