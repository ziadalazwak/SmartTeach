using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Domain.Interfaces
{
    public interface IGroupReposatory
    {
        public Task<IEnumerable<Student>> GetStudentsAttendaces(int id);
    }
}
