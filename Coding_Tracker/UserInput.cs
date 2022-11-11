using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace Coding_Tracker
{
    public class UserInput
    {
        public DateTime Date;
        public TimeSpan StartTime;
        public TimeSpan FinishTime;
        public void GetDateInput()
        {
            AnsiConsole.Markup("[bold]Please enter a date: [/]");
            string date = Console.ReadLine();

            while (!Validation.DateValidation(date))
            {
                AnsiConsole.MarkupLine("[red]Please try again: [/]");
                GetDateInput();
            }
            Date = DateTime.Parse(date);
        }

        public void GetTimeInput(ValidationType validationType)
        {
            switch (validationType)
            {
                case ValidationType.StartTime:
                    AnsiConsole.Markup("[bold]Please enter a start time: [/]");
                    string startTime = Console.ReadLine();

                    while (!Validation.TimeValidation(null,Date,ValidationType.StartTime, startTime))
                    {
                        AnsiConsole.MarkupLine("[red]Please try again: [/]");
                        GetTimeInput(ValidationType.StartTime);
                    }
                    StartTime = TimeSpan.Parse(startTime);
                    break;
                case ValidationType.FinishTime:
                    AnsiConsole.Markup("[bold]Please enter a finish time: [/]");
                    string finishTime = Console.ReadLine();

                    // Check if Finish Time is valid
                    while (!Validation.TimeValidation(StartTime,Date, ValidationType.FinishTime, finishTime))
                    {
                        AnsiConsole.MarkupLine("[red]Please try again: [/]");
                        GetTimeInput(ValidationType.FinishTime);
                    }
                    FinishTime = TimeSpan.Parse(finishTime);

                    break;
            }
        }
    }
}