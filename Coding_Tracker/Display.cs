using System.Net.Mime;
using System.Transactions;
using System.Linq;
using System.Collections.Generic;
using Coding_Tracker.Models;
using Spectre.Console;
using Coding_Tracker.Controllers;
using Coding_Tracker.Data;

namespace Coding_Tracker
{
    public  class Display
    {
        private readonly Db _db;
        public Display(Db db)
        {
            _db = db;
        }
        public void TableAllSessions(List<CodingSession> sessions)
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
                                 $"[black on grey82]{item.StartTime.ToString("HH:mm")}[/]",
                                 $"[black on grey82]{item.FinishTime.ToString("HH:mm")}[/]",
                                 $"[black on grey82]{item.Duration.ToString(@"hh\:mm")}[/]");
                }
                else
                {
                    table.AddRow(item.Id.ToString(),
                    item.Date.ToString("dd/MM/yyyy"),
                    item.StartTime.ToString("HH:mm"),
                    item.FinishTime.ToString("HH:mm"),
                    item.Duration.ToString(@"hh\:mm"));
                }
                rowNum++;
            }

            AnsiConsole.Write(table);
        }
        public void Menu()
        {
            string menu = string.Empty;
            while (menu != "Exit")
            {
            menu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Menu")
                    .PageSize(10)
                    .HighlightStyle(Style.Parse("blue"))
                    .AddChoices(new List<string>
                    {
                        "Add a new session",
                        "View all sessions",
                        "Select a session",
                        "Delete a session",
                        "Update a session",
                        "Exit"
                    }));

                if (menu == "Add a new session")
                {
                    AddSessionPrompt();
                }
                else if (menu == "View all sessions")
                {
                    TableAllSessions(SessionController.GetSessions(_db));
                }
                else if (menu == "Select a session")
                {
                    // TODO: Select a session
                }
                else if (menu == "Delete a session")
                {
                    // TODO: Delete a session
                }
                else if (menu == "Update a session")
                {
                    // TODO: Update a session
                }
                else if (menu == "Exit")
                {
                    AnsiConsole.MarkupLine("[red]Goodbye![/]");
                }
            }
        }

        public void AddSessionPrompt()
        {
            var date = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("Date")
                    .Validate(date => date.Date <= DateTime.Now.Date, "Date must be in the past")
                    .Validate(date => date.Date >= DateTime.Now.Date.AddDays(-7), "Date must be within the last 7 days")
                    .InvalidChoiceMessage("[red]Invalid date[/]"));

            DateTime startTime = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("Start Time")
                    .Validate(time => time.TimeOfDay <= DateTime.Now.TimeOfDay, "Time must be in the past")
                    .InvalidChoiceMessage("[red]Invalid time[/]"));

            DateTime finishTime = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("Finish Time")
                    .Validate(time => time.TimeOfDay <= DateTime.Now.TimeOfDay, "Time must be in the past")
                    .InvalidChoiceMessage("[red]Invalid time[/]"));

            var duration = finishTime - startTime;

            _db.Add(date, startTime, finishTime);
        }
    }
}