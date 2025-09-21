using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.App.Dto.SessionDto;
using SmartTeach.App.Interfaces;
using SmartTeach.App.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public class SessionMangmentService : ISessionMangmentService
    {
       private readonly IUnitOfWork _unitOfWork;
        public SessionMangmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAttendaceForSessionAsync(IEnumerable<AddAttendanceDto> addAttendanceDto,int sessionid)
        {
            var attendances =  addAttendanceDto.MapToDomains( sessionid);
                await _unitOfWork.Attendances.AddRangeAsync(attendances);   
            var commit = await _unitOfWork.CompleteAsync(); 
            if(commit <= 0)
                throw new InvalidOperationException("Could not add attendance for the session.");   


        }

        public async Task<AddSessionDto>  AddSession(int groupId, AddSessionDto addSessionDto)
        {
            var session= addSessionDto.MapToDomain(groupId);
           await _unitOfWork.Sessions.AddAsync(session);
            var commit= await _unitOfWork.CompleteAsync();
            if(commit <= 0)
                throw new InvalidOperationException("Could not create the session.");   


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
            var attendace =await _unitOfWork.Attendances.GetAllAsync(filter: a => a.SessionId == SessionID && a.StudentId == StudentId,includes:a=>a.Student);
            if (attendace == null || !attendace.Any())
                throw new ArgumentException($"No attendance found for session ID {SessionID} and student ID {StudentId}.");
            var attendanceDto = attendace.MapTDtos();
            return attendanceDto;
        }

        public async Task<IEnumerable<GetAttendaceDto>> GetAttendancesBySessionIdAsync(int sessionId)
        {
            var attendances = await _unitOfWork.Attendances.GetAllAsync(filter: a => a.SessionId == sessionId,includes:a=>a.Student);
            
            if (attendances == null || !attendances.Any())
                throw new ArgumentException($"No attendances found for session ID {sessionId}.");
            return attendances.MapTDtos();
        }

        public async Task<GetSessionDto> GetSessionByIdAsync(int sessionId)
        {
           var session= await _unitOfWork.Sessions.GetByIdAsync(sessionId);
            if(session== null)
                throw new ArgumentException($"Session with ID {sessionId} does not exist.");    
            var sessionDto = session.MapToDto(); 
            return sessionDto;
        }
    }
}
