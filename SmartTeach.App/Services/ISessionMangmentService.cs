using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.App.Dto.SessionDto;
using SmartTeach.App.Dto.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public interface ISessionMangmentService
    {
        // Define methods for session management here, e.g.:
       public  Task<AddSessionDto > AddSession(int groupId,AddSessionDto addSessionDto);
 
        public Task<IEnumerable<GetSessionDto>> GetAllSessionsAsync(SessionRequestQuery query);
        public Task<GetSessionDto> GetSessionByIdAsync(int sessionId);

        public Task<GetStudentDto> AddAttendaceForSessionAsync(AddAttendanceDto addAttendanceDto,int sessionid);
        public Task<IEnumerable<GetAttendaceDto>> GetAttendancesBySessionIdAsync(int sessionId);
          public Task<IEnumerable<GetAttendaceDto>> GetAttendanceBySessionAndStudentIdAsync(int SessionID,int StudentId);

        public Task<IEnumerable<GetAttendaceDto>> GetSessionAttendanceDisplayAsync(int sessionId);
        public  Task<GetAttendaceDto> ToggaleAttendace(int AttendaceId);    
        public Task<GetStudentDto> AddAttendaceForNewStudent(AddAttendanceDto addAttendanceDto, int sessionId);

    }
}
