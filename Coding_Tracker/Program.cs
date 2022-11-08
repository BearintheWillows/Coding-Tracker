using Coding_Tracker.Data;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Configuration;


// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File( "Logs/log.txt" )
    .MinimumLevel.Debug()
    .CreateLogger();

var config = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("Secrets.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>()
			.Build();

var connectionString = config.GetConnectionString("DefaultConnection");

var db = new Db( connectionString );

db.CreateDatabase();