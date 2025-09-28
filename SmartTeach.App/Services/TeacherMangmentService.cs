using SmartTeach.App.Dto.StudentDto;
using SmartTeach.App.Interfaces;
using SmartTeach.App.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public class TeacherMangmentService:ITeacherMangmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TeacherMangmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public async Task<IEnumerable<GetStudentDto>> GetStudentsForTeacher(string teacherId)
        {
          var TeacherGroups=  await _unitOfWork.Groups.GetAllAsync(g => g.ApplicationUserId == teacherId,null,a=>a.GroupStudents,a=>a.GroupStudents.Select(g=>g.Student) );
            if(TeacherGroups==null )
                throw new ArgumentException($"No groups found for teacher with ID {teacherId}.");
            var students = TeacherGroups.SelectMany(g => g.GroupStudents)
                .Select(sg => sg.Student)
                .Distinct()
                .ToList();
            var studentDtos = students.MapToGetStudentDtos();
            return studentDtos; 

        }
    }
}
