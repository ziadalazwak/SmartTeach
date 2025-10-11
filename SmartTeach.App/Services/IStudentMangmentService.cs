using SmartTeach.App.Dto.ParentInfoDto;
using SmartTeach.App.Dto.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public interface IStudentMangmentService
    {
        public  IEnumerable<GetStudentDto> GetStudentsForTeacher(string teacherId);
        public Task<StudentInfoForParentDto>GetStudentInfoForParent(int studentId);
    }
}
