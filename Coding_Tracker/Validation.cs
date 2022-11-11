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
                AnsiConsole.MarkupLine("[red]Invalid date format. Please enter date in the format dd/mm/yyyy[/]");
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
    }
}