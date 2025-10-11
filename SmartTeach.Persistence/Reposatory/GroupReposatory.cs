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
    public class GroupReposatory:IGroupReposatory
    {
        private readonly SmartTeachDbcontext smartTeachDbcontext;
        public GroupReposatory(SmartTeachDbcontext smartTeachDbcontext)
        {
            this.smartTeachDbcontext = smartTeachDbcontext;
        }
 
        public async Task<IEnumerable<Student>> GetStudentsAttendaces(int id)
        {
            var studentGroup = await smartTeachDbcontext.StudentGroups
                .Where(sg => sg.GroupId == id)
                .Include(sg => sg.Student).ThenInclude(s => s.Attendances)
                .ToListAsync();
            if (studentGroup == null)
                throw new ArgumentException($"StudentGroup with ID {id} does not exist.");
            var students = studentGroup.Select(s => s.Student).ToList();
 return students;
        }
    }
}
