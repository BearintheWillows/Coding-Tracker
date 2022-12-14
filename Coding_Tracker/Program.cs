using Coding_Tracker.Data;
using Microsoft.Extensions.Configuration;
using Serilog;
using Coding_Tracker;
using Spectre.Console;
using Coding_Tracker.Controllers;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
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
RunProgram();

void RunProgram()
{
    string menu = string.Empty;
    while (menu != "Exit")
    {
        menu = Display.Menu();
        switch (menu)
        {
            case "Add a new session":
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[bold blue underline]Add a new session[/]");
                AnsiConsole.WriteLine();
                UserInput input = new();
                input.GetDateInput();
                input.GetTimeInput(ValidationType.StartTime);
                input.GetTimeInput(ValidationType.FinishTime);
                SessionController.AddSession(db, input);
                break;
            case "View all sessions":
                AnsiConsole.Clear();
                var ruleViewAll = new Rule("[bold blue underline]View all sessions[/]");
                AnsiConsole.Write(ruleViewAll);
                AnsiConsole.WriteLine();
                Display.TableAllSessions(SessionController.ViewAllSessions(db));
                break;
            case "View a session":
                AnsiConsole.Clear();
                var ruleView = new Rule("[bold blue]View a Session[/]");
                AnsiConsole.Write(ruleView);
                AnsiConsole.WriteLine();
                UserInput userInput = new();
                userInput.GetIdInput();
                SessionController.ViewSessionById(db, userInput.Id);
                break;
            case "Update a session":
                AnsiConsole.Clear();
                var ruleUpdate = new Rule("[bold blue underline]Update a session[/]");
                AnsiConsole.Write(ruleUpdate);
                AnsiConsole.WriteLine();
                UserInput updateInput = new();
                updateInput.GetIdInput();
                updateInput.GetDateInput();
                updateInput.GetTimeInput(ValidationType.StartTime);
                updateInput.GetTimeInput(ValidationType.FinishTime);
                SessionController.UpdateSession(db, updateInput.Id, updateInput);
                break;
            case "Delete a session":
                AnsiConsole.Clear();
                var ruleDelete = new Rule("[bold blue]Delete a session[/]");
                AnsiConsole.Write(ruleDelete);
                AnsiConsole.WriteLine();
                UserInput deleteInput = new();
                deleteInput.GetIdInput();
                SessionController.DeleteSession(db, deleteInput.Id);
                break;
            case "Exit":
            break;
        }
    }
}


// if (menu == "Add a new session")
//                 {
//                     AddSessionPrompt();
//                 }
//                 else if (menu == "View all sessions")
//                 {
//                     TableAllSessions(SessionController.GetSessions(_db));
//                 }
//                 else if (menu == "Select a session")
//                 {
//                     // TODO: Select a session
//                 }
//                 else if (menu == "Delete a session")
//                 {
//                     // TODO: Delete a session
//                 }
//                 else if (menu == "Update a session")
//                 {
//                     // TODO: Update a session
//                 }
//                 else if (menu == "Exit")
//                 {
//                     AnsiConsole.MarkupLine("[red]Goodbye![/]");
//                 }
//             }

// Regex dateFormat = new Regex("^[0-9]{2}\\/[0-9]{2}\\/[0-9]{4}$");

// Console.WriteLine(dateFormat.IsMatch(Console.ReadLine()));