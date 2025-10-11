using Microsoft.EntityFrameworkCore;
using SmartTeach.Domain.Interfaces;
using SmartTeach.Domain.Models;
using SmartTeach.Persistence.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Persistence.Reposatory
{
    public class SessionReposatory:GenericReposatory<Session>,ISessionReposatory
    {
        private readonly SmartTeachDbcontext _context;  
        public SessionReposatory(SmartTeachDbcontext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Attendance>> SessionAttendace(int sessionId)
        {
            var session = await _context.Sessions.FindAsync(sessionId);
            if (session == null)
                throw new ArgumentException($"Session with ID {sessionId} does not exist.");
            var attendances = await _context.Attendances
                .Where(a => a.SessionId == sessionId).Include(a => a.Student)   
                .ToListAsync();
            return attendances;
        }   
    }
}
