using Coding_Tracker.Data;
using Coding_Tracker.Models;
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
db.Add( new DateTime( 2021, 10, 10 ), new DateTime( 2021, 10, 10, 10, 0, 0 ), new DateTime( 2021, 10, 10, 11, 0, 0 ) );
var cs = db.GetById(1);
cs.Duration = new TimeSpan(3, 23, 0);


db.Update( cs );