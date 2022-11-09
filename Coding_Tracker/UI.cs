using System.Collections.Generic;
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
                    table.AddRow($"[default on grey82]{item.Id}[/]",
                                 $"[default on grey82]{item.Date.ToString("dd/MM/yyyy")}[/]",
                                 $"[default on grey82]{item.StartTime.ToString("HH:mm")}[/]",
                                 $"[default on grey82]{item.FinishTime.ToString("HH:mm")}[/]",
                                 $"[default on grey82]{item.Duration.ToString(@"hh\:mm")}[/]");
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
    }
}