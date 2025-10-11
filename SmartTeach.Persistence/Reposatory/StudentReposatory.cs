using Microsoft.EntityFrameworkCore;
using SmartTeach.App.Dto.StudentDto;
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
    public class StudentReposatory : IStudentReposatory
    {
        private readonly SmartTeachDbcontext smartTeachDbcontext;
        public StudentReposatory(SmartTeachDbcontext smartTeachDbcontext)
        {
            this.smartTeachDbcontext = smartTeachDbcontext;
        }
        public IEnumerable<Student> GetStudentForTeacher(string teacherId)
        {
            var TeacherGroups = smartTeachDbcontext.Groups
                .Where(g => g.ApplicationUserId == teacherId)
                .Include(g => g.GroupStudents)
                .ThenInclude(gs => gs.Student);
            return TeacherGroups.SelectMany(g => g.GroupStudents)
                .Select(sg => sg.Student)
                .Distinct()
                .ToList();

        }


        public async Task<Student> GetStudentParentInfo(int studentId)
        {
            var studentInfo = await smartTeachDbcontext.Students
                         .Include(s => s.Attendances)
                    .ThenInclude(a => a.Session)
                        .Include(a=>a.StudentGroups).ThenInclude(b=>b.Group)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            return studentInfo;
        }
       

    }
}
