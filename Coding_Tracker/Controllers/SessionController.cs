using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coding_Tracker.Data;
using Coding_Tracker.Models;

namespace Coding_Tracker.Controllers
{
    public static class SessionController
    {
    
        public static List<CodingSession> GetSessions(Db db)
        {
            return db.GetAll().ToList();
        }
    }
}