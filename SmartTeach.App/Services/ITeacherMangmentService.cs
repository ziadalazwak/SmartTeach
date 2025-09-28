using SmartTeach.App.Dto.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public interface ITeacherMangmentService
    {
        public  Task<IEnumerable<GetStudentDto>> GetStudentsForTeacher(string teacherId);
    }
}
