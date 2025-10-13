using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.App.Dto.SessionDto;
using SmartTeach.App.Dto.StudentAttendaceDto;
using SmartTeach.App.Dto.StudentDto;
using SmartTeach.App.Interfaces;
using SmartTeach.App.Mapping;
using SmartTeach.Domain.Interfaces;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public class SessionMangmentService : ISessionMangmentService
    {
        private readonly ISessionReposatory _sessionReposatory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupReposatory _groupReposatory;
        public SessionMangmentService(IUnitOfWork unitOfWork, IGroupReposatory groupReposatory,ISessionReposatory sessionReposatory)
        {
            _unitOfWork = unitOfWork;
            _groupReposatory = groupReposatory;
            _sessionReposatory = sessionReposatory;

        }
        public async Task<GetStudentDto> AddAttendaceForSessionAsync(AddAttendanceDto addAttendanceDto, int sessionid)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(addAttendanceDto.StudentId, includes: a => a.Attendances.Where(a => a.SessionId==sessionid));
            if (student == null) throw new InvalidOperationException(" Id is invalid ");
            if (student.Attendances != null && student.Attendances.Any(a => a.SessionId == sessionid))
            {
                if (student.Attendances.FirstOrDefault().IsPresent==true)
                    throw new InvalidOperationException("Attendance for this student in this session already exists.");
                else
                {
                    student.Attendances.FirstOrDefault().IsPresent = true;
                    _unitOfWork.Students.Update(student);
                    var commit1 = await _unitOfWork.CompleteAsync();
                    if (commit1 <= 0)
                        throw new InvalidOperationException("Could not add attendance for the session.");
                    return student.MapToGetStudentDto();
                }
            }





            return student.MapToGetStudentDto();

        }

        public async Task<GetStudentDto> AddAttendaceForNewStudent(AddAttendanceDto addAttendanceDto, int sessionid)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(addAttendanceDto.StudentId, includes: a => a.Attendances.Where(a => a.SessionId==sessionid));
           
            if (student == null) throw new InvalidOperationException(" Id is invalid ");
            if (student.Attendances != null && student.Attendances.Any(a => a.SessionId == sessionid))
            {
                throw new InvalidOperationException("Attendance for this student in this session already exists.");
            }
         var newAttendace= new Attendance
            {
                StudentId = addAttendanceDto.StudentId,
                IsPresent = true,
                SessionId = sessionid
          };

            await _unitOfWork.Attendances.AddAsync(newAttendace);
            var Complete=await _unitOfWork.CompleteAsync();
            if (Complete <= 0)
                throw new InvalidOperationException("Could not add attendance for the session.");





            return student.MapToGetStudentDto();

        }

        public async Task<AddSessionDto > AddSession(int groupId, AddSessionDto addSessionDto)
        {
         
            var session = addSessionDto.MapToDomain(groupId);
            await _unitOfWork.Sessions.AddAsync(session);
            var commit = await _unitOfWork.CompleteAsync();
            if (commit <= 0)
                throw new InvalidOperationException("Could not create the session.");
            var students = await _unitOfWork.StudentsGroups.GetAllAsync(s => s.GroupId==groupId);
            await _unitOfWork.Attendances.AddRangeAsync(students.Select(s => new Attendance
            {
                StudentId = s.StudentId,
                IsPresent = false,
                SessionId =session.Id
            }));
            commit = await _unitOfWork.CompleteAsync();
            if (commit <= 0)
                throw new InvalidOperationException("Could not create the attendance for the session.");

            return addSessionDto;
        }

        public async Task<IEnumerable<GetSessionDto>> GetAllSessionsAsync(SessionRequestQuery query)
        {
            var sessions = _unitOfWork.Sessions.Query();

            if (query.GroupId > 0)
                sessions = sessions.Where(s => s.GroupId == query.GroupId);

            if (query.Year > 0)
                sessions = sessions.Where(s => s.StartTime.Year == query.Year);

            if (query.Month > 0)
                sessions = sessions.Where(s => s.StartTime.Month == query.Month);

            if (query.Day > 0)
                sessions = sessions.Where(s => s.StartTime.Day == query.Day);



            if (sessions == null || !sessions.Any())
                throw new ArgumentException($"No sessions found for this Query.");

            var sessionDtos = sessions.MapToDtos();
            return sessionDtos;
        }


        public async Task<IEnumerable<GetAttendaceDto>> GetAttendanceBySessionAndStudentIdAsync(int SessionID, int StudentId)
        {
            //categorize the repos create arepo and add this methid and call it here
            var attendace = await _unitOfWork.Attendances.GetAllAsync(filter: a => a.SessionId == SessionID && a.StudentId == StudentId, includes: a => a.Student);
            if (attendace == null || !attendace.Any())
                throw new ArgumentException($"No attendance found for session ID {SessionID} and student ID {StudentId}.");
            var attendanceDto = attendace.MapTDtos();
            return attendanceDto;
        }

        public async Task<IEnumerable<GetAttendaceDto>> GetAttendancesBySessionIdAsync(int sessionId)
        {
            var attendances = await _unitOfWork.Attendances.GetAllAsync(filter: a => a.SessionId == sessionId, includes: a => a.Student);

            if (attendances == null || !attendances.Any())
                throw new ArgumentException($"No attendances found for session ID {sessionId}.");
            return attendances.MapTDtos();
        }
        public async Task<GetAttendaceDto> ToggaleAttendace(int AttendaceId)
        {
            var attendance = await _unitOfWork.Attendances.GetByIdAsync(AttendaceId);
            if (attendance == null)
                throw new ArgumentException($"Attendance with ID {AttendaceId} does not exist.");
            attendance.IsPresent = !attendance.IsPresent;
            _unitOfWork.Attendances.Update(attendance);
            var commit = await _unitOfWork.CompleteAsync();
            if (commit <= 0)
                throw new InvalidOperationException("Could not update the attendance.");
            var attendaceDto=new GetAttendaceDto { IsPresent = attendance.IsPresent, StudentId = attendance.StudentId, SessionId = attendance.SessionId, Date = attendance.Date, Id = attendance.Id };
            return attendaceDto;
        }
        
        public async Task<GetSessionDto> GetSessionByIdAsync(int sessionId)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(sessionId);
            if (session== null)
                throw new ArgumentException($"Session with ID {sessionId} does not exist.");
            var sessionDto = session.MapToDto();
            return sessionDto;
        }
        public async Task<IEnumerable<StudentForAttendaceDto>> GetStudentsAttendaceId(int id, int sessionId)
        {
            var students = await _groupReposatory.GetStudentsAttendaces(id);
            if (students == null)
                throw new ArgumentException($"StudentGroup with ID {id} does not exist.");
            var StudentForAttendace = students.MapToStudentAttendacesDtos();
            return StudentForAttendace;
        }

        public async Task<IEnumerable<GetAttendaceDto>> GetSessionAttendanceDisplayAsync(int sessionId)
        {
          var Attendace=await _sessionReposatory.SessionAttendace(sessionId);
            if (Attendace == null || !Attendace.Any())
                throw new ArgumentException($"No attendances found for session ID {sessionId}.");
          
           var attendaceDto = Attendace.MapTDtos();
            return attendaceDto;
        }
    }
}