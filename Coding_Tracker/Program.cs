using Coding_Tracker.Data;
using Microsoft.Extensions.Configuration;
using Serilog;
using Coding_Tracker;


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
var display = new Display(db);

db.CreateDatabase();
display.Menu();
