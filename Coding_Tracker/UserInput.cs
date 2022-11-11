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

            bool isDateValid = Validation.DateValidation(date);

             if (!isDateValid)
             {
                AnsiConsole.MarkupLine("[red]Please try again: [/]");
                GetDateInput();
             }
             else
             {
                Date = DateTime.Parse(date);
             }
        }


        public void GetTimeInput(ValidationType validationType)
        {
            switch (validationType)
            {
                case ValidationType.StartTime:
                    AnsiConsole.Markup("[bold]Please enter a start time: [/]");
                    string startTime = Console.ReadLine();

                    if (!Validation.TimeValidation(null, Date, validationType, startTime))
                    {
                        AnsiConsole.MarkupLine("[red]Please try again: [/]");
                        GetTimeInput(validationType);
                    }
                    else
                    {
                        StartTime = TimeSpan.Parse(startTime);
                    }
                    break;
                case ValidationType.FinishTime:
                    AnsiConsole.Markup("[bold]Please enter a finish time: [/]");
                    string finishTime = Console.ReadLine();

                    if (true)
                    {
                        if (!Validation.TimeValidation(StartTime, Date, validationType, finishTime))
                        {
                            AnsiConsole.MarkupLine("[red]Please try again: [/]");
                            GetTimeInput(validationType);
                        }
                        else
                        {
                            FinishTime = TimeSpan.Parse(finishTime);
                        }
                    }
                    break;
            }
            return;
        }
    }
}