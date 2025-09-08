using Microsoft.AspNetCore.Mvc;
using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.App.Dto.SessionDto;
using SmartTeach.App.Services;

namespace SmartTeach.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionMangmentController:ControllerBase
    {
        private readonly ISessionMangmentService sessionMangmentService;
        public SessionMangmentController(ISessionMangmentService sessionMangmentService)
        {
            this.sessionMangmentService = sessionMangmentService;
        }
        [HttpGet]
        [Route("GetAllSessions/{groupId}")]
        public async Task<IActionResult> GetAllSessions(int groupId)
        {
            var sessions = await sessionMangmentService.GetAllSessionsAsync(groupId);
            return Ok(sessions);
        }
        [HttpGet]
        [Route("GetSessionById/{sessionId}")]
        public async Task<IActionResult> GetSessionById(int sessionId)
        {
            var session = await sessionMangmentService.GetSessionByIdAsync(sessionId);
            if (session == null)
                return NotFound($"Session with ID {sessionId} not found.");
            return Ok(session);
        }
        [HttpPost]
        [Route("AddSession/{groupId}")]
        public async Task<IActionResult> AddSession(int groupId, [FromBody] AddSessionDto addSessionDto)
        {
            if (addSessionDto == null)
                return BadRequest("Session data is required.");
            var createdSession = await sessionMangmentService.AddSession(groupId, addSessionDto);
            return Ok("CREATED SUCESSFULY");
        }
        [HttpPost]
        [Route("AddAttendaceForSession/{sessionId}")]
        public async Task<IActionResult> AddAttendaceForSession(int sessionId, [FromBody] IEnumerable<AddAttendanceDto> addAttendanceDto)
        {
            if (addAttendanceDto == null || !addAttendanceDto.Any())
                return BadRequest("Attendance data is required.");
            await sessionMangmentService.AddAttendaceForSessionAsync(addAttendanceDto, sessionId);
            return Ok("ATTENDANCE ADDED SUCESSFULY");
        }
        [HttpGet]

        [Route("GetAttendancesBySessionId/{sessionId}")]
        public async Task<IActionResult> GetAttendancesBySessionId(int sessionId)
        {
            var attendances = await sessionMangmentService.GetAttendancesBySessionIdAsync(sessionId);
            if (attendances == null || !attendances.Any())
                return NotFound($"No attendances found for session ID {sessionId}.");
            return Ok(attendances);
        }
        [HttpGet]
        [Route("GetAttendanceBySessionAndStudentId/{sessionId}/{studentId}")]
        public async Task<IActionResult> GetAttendanceBySessionAndStudentId(int sessionId, int studentId)
        {
            var attendance = await sessionMangmentService.GetAttendanceBySessionAndStudentIdAsync(sessionId, studentId);
            if (attendance == null || !attendance.Any())
                return NotFound($"No attendance found for session ID {sessionId} and student ID {studentId}.");
            return Ok(attendance);
        }
    }
}
