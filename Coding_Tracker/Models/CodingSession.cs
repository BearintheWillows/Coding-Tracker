using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Tracker.Models;
public class CodingSession
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime FinishTime { get; set; }
    public TimeSpan Duration { get; set; }
}
