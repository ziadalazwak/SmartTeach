using SmartTeach.App.Interfaces;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Domain.Interfaces
{
    public interface ISessionReposatory:IGenericReposatory<Session>
    {
        public Task<IEnumerable<Attendance>> SessionAttendace(int sessionId);

    }
}
