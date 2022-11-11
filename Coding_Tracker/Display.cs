using Coding_Tracker.Models;
using Spectre.Console;

namespace Coding_Tracker
{
    public static class Display
    {
        public static void TableAllSessions(List<CodingSession> sessions)
        {
            var table = new Table();
            var rowNum = 1;
            table.AddColumn("Id");
            table.AddColumn("Date");
            table.AddColumn("Start Time");
            table.AddColumn("Finish Time");
            table.AddColumn("Duration");

            foreach (var item in sessions)
            {
                if (rowNum % 2 == 0)
                {
                    table.AddRow($"[black on grey82]{item.Id}[/]",
                                 $"[black on grey82]{item.Date.ToString("dd/MM/yyyy")}[/]",
                                 $"[black on grey82]{item.StartTime.ToString()}[/]",
                                 $"[black on grey82]{item.FinishTime.ToString()}[/]",
                                 $"[black on grey82]{item.Duration.ToString()}[/]");
                }
                else
                {
                    table.AddRow(item.Id.ToString(),
                    item.Date.ToString("dd/MM/yyyy"),
                    item.StartTime.ToString(),
                    item.FinishTime.ToString(),
                    item.Duration.ToString());
                }
                rowNum++;
            }

            AnsiConsole.Write(table);
        }
        public static string Menu()
        {
            var rule = new Rule("[bold blue]Menu[/]");
            AnsiConsole.Write(rule);
            var menu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .HighlightStyle(Style.Parse("blue"))
                    .AddChoices(new List<string>
                    {
                        "Add a new session",
                        "View all sessions",
                        "View a session",
                        "Update a session",
                        "Delete a session",
                        "Exit"
                    }));
            return menu;
        }

    
        public static void ValidationError()
        {
            AnsiConsole.MarkupLine("[red]Invalid date format. Please enter date in the format dd/mm/yyyy[/]");

        }
        public static void AddSessionPrompt()
        {
        
            // DateTime startTime = AnsiConsole.Prompt(
            //     new TextPrompt<DateTime>("Start Time")
            //         .Validate(time => time.TimeOfDay >= DateTime.Now.TimeOfDay, "Time must be in the past")
            //         .Validate(time => DateTime.TryParseExact(time.ToShortTimeString(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time), "Time must be in the format HH:mm")
            //         .InvalidChoiceMessage("[red]Invalid start time[/]"));

            // DateTime finishTime = AnsiConsole.Prompt(
            //     new TextPrompt<DateTime>("Finish Time")
            //         .Validate(time => time.TimeOfDay <= DateTime.Now.TimeOfDay, "Time must be in the past")
            //         .Validate(time => time > startTime, "Finish time must be after start time")
            //         .Validate(time => DateTime.TryParseExact(time.ToShortTimeString(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time), "Time must be in the format HH:mm")
            //         .InvalidChoiceMessage("[red]Invalid finish time[/]"));

            // var duration = finishTime - startTime;

            // _db.Add(DateTime.Parse(date), startTime, finishTime);
        }
    }
}