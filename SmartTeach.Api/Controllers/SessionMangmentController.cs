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
    
        public async Task<IActionResult> GetAllSessions([FromQuery] SessionRequestQuery query)
        {
            var sessions = await sessionMangmentService.GetAllSessionsAsync( query);
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
        [Route("AddAttendaceForNewStudent/{sessionId}")]
        public async Task<IActionResult> AddAttendaceForNewStudent(int sessionId, [FromBody] AddAttendanceDto addAttendanceDto)
        {
            if (addAttendanceDto.StudentId < 0)
                return BadRequest("Attendance data is wrong.");
            try
            {
                var student = await sessionMangmentService.AddAttendaceForNewStudent(addAttendanceDto, sessionId);
                return Ok(student);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
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
        [HttpPatch]
        [Route("ToggaleAttendace/{AttendaceId}")]
        public async Task<IActionResult> ToggaleAttendace(int AttendaceId)
        {
            
            var updatedAttendance = await sessionMangmentService.ToggaleAttendace(AttendaceId);
            if (updatedAttendance==null)
                return NotFound($"Attendance with ID {AttendaceId} not found.");
            return Ok( updatedAttendance);
        }
        [HttpPost]
        [Route("AddAttendaceForSession/{sessionId}")]
        public async Task<IActionResult> AddAttendaceForSession(int sessionId, [FromBody] AddAttendanceDto addAttendanceDto)
        {
            if (addAttendanceDto.StudentId<0)
                return BadRequest("Attendance data is wrong.");
          var student=  await sessionMangmentService.AddAttendaceForSessionAsync(addAttendanceDto, sessionId);
            return Ok(student);
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

        [HttpGet]
        [Route("GetSessionAttendanceDisplay/{sessionId}")]
        public async Task<IActionResult> GetSessionAttendanceDisplay(int sessionId)
        {
            try
            {
                var sessionAttendance = await sessionMangmentService.GetSessionAttendanceDisplayAsync(sessionId);
                return Ok(sessionAttendance);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving session attendance: {ex.Message}");
            }
        }
    }
}
