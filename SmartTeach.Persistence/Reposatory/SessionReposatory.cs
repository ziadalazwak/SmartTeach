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
        public SessionReposatory(SmartTeachDbcontext context) : base(context)
        {
        }
    }
}
