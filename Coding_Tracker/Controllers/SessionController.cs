using Coding_Tracker.Data;
using Coding_Tracker.Models;

namespace Coding_Tracker.Controllers;
    public static class SessionController
    {
        public static List<CodingSession> GetSessions(Db db)
        {
            return db.GetAll().ToList();
        }

        public static void AddSession(Db db, UserInput input)
        {
            db.Add(input.Date, input.StartTime, input.FinishTime);
        }
    }
