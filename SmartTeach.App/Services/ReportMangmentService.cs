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
    public class ReportMangmentService:IReprotMangmentService
    {
        private readonly IUnitOfWork unitOfWork;
        public ReportMangmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<byte[]> GenerateStudentReportAsync(int studentId)
        {
            var studentAttendances = await unitOfWork.Students.GetByIdAsync(studentId, includes: a => a.Attendances);
          var studentGroup= await unitOfWork.StudentsGroups.GetAllAsync(filter: sg => sg.StudentId == studentId, includes: sg => sg.Group);


           if(studentGroup==null )
            {
                throw new InvalidOperationException($"No Groups of this student {studentId}.");
            }
           
                var groupNames = string.Join(", ", studentGroup.Select(sg => sg.Group.Name));
                var numberOfGroups = studentGroup.Count();
         
             
           
            if (studentAttendances == null)
                throw new ArgumentException($"Student with ID {studentId} does not exist.");

            if (studentAttendances.Attendances == null || !studentAttendances.Attendances.Any())
                throw new InvalidOperationException($"No attendance records found for student with ID {studentId}.");

            var totalSessions = studentAttendances.Attendances.Count;
            var attendedSessions = studentAttendances.Attendances.Count(a => a.IsPresent);
            var attendancePercentage = (double)attendedSessions / totalSessions * 100;

            // Map to your DTO (assuming you have this)
            var studentDto = studentAttendances.MapToGetStudentDto();

            // Build a simple text report (later you can replace this with PDF)
            var content = $@"
Student Report
==============
Name: {studentDto.FirstName}
Last Name: {studentDto.LastName}
ID: {studentDto.Id}
Groups: {groupNames}
NUmber of Groups: {numberOfGroups}  
Total Sessions: {totalSessions}
Attended Sessions: {attendedSessions}
Attendance %: {attendancePercentage:F2}
";

            // Convert the string content into a byte array (UTF8 encoding)
            return System.Text.Encoding.UTF8.GetBytes(content);
        }
        public async Task<byte[]> GenerateSessionReportAsync(int sessionId)
        {
            var session = await unitOfWork.Sessions.GetByIdAsync(sessionId, includes: s => s.Attendances);
            if (session == null)
                throw new ArgumentException($"Session with ID {sessionId} does not exist.");
            if (session.Attendances == null || !session.Attendances.Any())
                throw new InvalidOperationException($"No attendance records found for session with ID {sessionId}.");
            var totalStudents = session.Attendances.Count;
            var presentStudents = session.Attendances.Count(a => a.IsPresent);
            var attendancePercentage = (double)presentStudents / totalStudents * 100;
            // Map to your DTO (assuming you have this)
            var sessionDto = session.MapToDto();
            // Build a simple text report (later you can replace this with PDF)
            var content = $@"
Student Report
==============
Name: {sessionDto.Topic}

ID: {sessionDto.Id}
Start Time: {sessionDto.StartTime}    
 Sessions  total students: {totalStudents}
presentStudents: {presentStudents}

Attendance %: {attendancePercentage:F2}
";

            // Convert the string content into a byte array (UTF8 encoding)
            return System.Text.Encoding.UTF8.GetBytes(content);

    }
        public async Task<IEnumerable<GetStudentDtoforReport>> GetStudentDetailsForReportAsync(string studentName)
        {
            var student = await unitOfWork.Students.GetAllAsync(filter: s => s.FirstName.Contains(studentName) || s.LastName.Contains(studentName));
            if (student == null || !student.Any())
                throw new ArgumentException($"No student found with name containing '{studentName}'.");
            var studentDtos = student.MapToGetStudentDtoforReport();
            return studentDtos ; // Return the first matching student
        }
    }
}
