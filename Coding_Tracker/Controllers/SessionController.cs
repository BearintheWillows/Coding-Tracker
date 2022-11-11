using Coding_Tracker.Data;
using Coding_Tracker.Models;
using Spectre.Console;
namespace Coding_Tracker.Controllers;
    public static class SessionController
    {
        public static List<CodingSession> ViewAllSessions(Db db)
        {
            return db.GetAll().ToList();
        }

        public static void AddSession(Db db, UserInput input)
        {
            db.Add(input.Date, input.StartTime, input.FinishTime);
        }

        public static void DeleteSession(Db db, int id)
        {
            // db.Delete(id);
        }

        public static void UpdateSession(Db db, int id, UserInput input)
        {
            CodingSession session = new()
            {
                Id = id,
                Date = input.Date,
                StartTime = input.StartTime,
                FinishTime = input.FinishTime
            };
            db.Update(session);
        }

        public static void ViewSessionById(Db db, int id)
        {
            int inputId = id;
            CodingSession? session = db.GetById(inputId);
            if (session != null)
            {
                AnsiConsole.MarkupLine($"[bold green]Date:[/] {session?.Date}, [bold]Start time:[/] {session?.StartTime}, [bold]Finish time:[/] {session?.FinishTime}, [bold]Duration:[/] {session?.Duration}");
                AnsiConsole.WriteLine();
                var rule = new Rule("[bold blue]Press any key to return to the menu[/]");
                AnsiConsole.Write(rule);
                Console.ReadKey();
            }
            else
            {
                AnsiConsole.MarkupLine($"[bold red]No session with id {inputId} found[/]");
                AnsiConsole.WriteLine();
            }
        }
    }
