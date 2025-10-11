using SmartTeach.App.Dto.ParentInfoDto;
using SmartTeach.App.Dto.StudentDto;
using SmartTeach.App.Interfaces;
using SmartTeach.App.Mapping;
using SmartTeach.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public class StudentMangmentService:IStudentMangmentService
    {
        private readonly IStudentReposatory _studentReposatory;
        private readonly ITeacherReposatroy _teacherReposatroy;
        private readonly IUnitOfWork _unitOfWork;   

        public StudentMangmentService(IStudentReposatory studentReposatory, IUnitOfWork unitOfWork,ITeacherReposatroy teacherReposatroy)
        {
          _studentReposatory=studentReposatory;
            _teacherReposatroy= teacherReposatroy;
            _unitOfWork=unitOfWork;

        }
        public  IEnumerable<GetStudentDto> GetStudentsForTeacher(string teacherId)
        {
          
            var TeacherStudents = _studentReposatory.GetStudentForTeacher(teacherId);   
            var studentDtos =   TeacherStudents.MapToGetStudentDtos();  
            return studentDtos; 
        }
        public async Task<StudentInfoForParentDto> GetStudentInfoForParent(int studentId)
        {
         var StudentInfo= await _studentReposatory.GetStudentParentInfo(studentId);   
            if (StudentInfo == null)
                    throw new InvalidOperationException("Student not found.");

            var TeacherId= StudentInfo.StudentGroups.FirstOrDefault().Group.ApplicationUserId;
            if(TeacherId==null)
                throw new InvalidOperationException("Teacher ID not found for the student's group.");
            var TeacherInfo= await _teacherReposatroy.GetTeacherInfoForParentAsync(TeacherId);  
     
    
            var studentInfoDtos = StudentInfo.MapToStudentInfoForParentDto(TeacherInfo);  
            return studentInfoDtos;
        }
  

    }
}
