using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Spectre.Console;
using Serilog;

namespace Coding_Tracker
{
    public static class Validation
    {
        public static bool DateValidation(string date)
        {
            Regex dateFormat = new Regex("^[0-9]{2}\\/[0-9]{2}\\/[0-9]{4}$");
            DateTime ParsedDate = new DateTime();

            try
            {
                _ = DateTime.TryParse(date, out ParsedDate);
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine("[red]Not a Date. Please try again.[/]");
                Log.Warning(e, "Invalid date format");
            }

            if (!dateFormat.IsMatch(date))
            {
                AnsiConsole.MarkupLine(
                    "[red]Invalid date format. Please enter date in the format dd/mm/yyyy[/]"
                );
                return false;
            }
            if (ParsedDate.Date > DateTime.Today)
            {
                AnsiConsole.MarkupLine("[red]Date must be today or in the past[/]");
                return false;
            }
            if (ParsedDate <= DateTime.Now.AddDays(-7))
            {
                AnsiConsole.MarkupLine("[red]Date must be within 7 days[/]");
                return false;
            }

            return true;
        }

        internal static bool TimeValidation(
            TimeSpan? startTime,
            DateTime? date,
            ValidationType validationType,
            string? time
        )
        {
            TimeSpan ParsedTime = new();

            try
            {
                _ = TimeSpan.TryParse(time, out ParsedTime);
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine("[red]Not a Date. Please try again.[/]");
                Log.Warning(e, "Invalid Time Format");
            }

            switch (validationType)
            {
                case ValidationType.StartTime:
                    if (date == DateTime.Today)
                    {
                        if (ParsedTime > TimeSpan.Parse(DateTime.Now.ToLongTimeString()))
                        {
                            AnsiConsole.MarkupLine("[red]Start time must be in the past[/]");
                            return false;
                        }
                    }
                    break;
                case ValidationType.FinishTime:
                    if (ParsedTime < startTime)
                    {
                        AnsiConsole.MarkupLine("[red]Finish time must be after start time[/]");
                        return false;
                    }
                    break;
            }
            return true;
        }
    }
}
