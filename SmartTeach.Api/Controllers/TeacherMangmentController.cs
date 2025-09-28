using Microsoft.AspNetCore.Mvc;
using SmartTeach.App.Services;

namespace SmartTeach.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherMangmentController: ControllerBase
    {
        private readonly ITeacherMangmentService _teacherMangmentService;
        public TeacherMangmentController(ITeacherMangmentService teacherMangmentService)
        {
            _teacherMangmentService=teacherMangmentService;
        }
        [HttpGet("students/{teacherId}")]   
        public async Task<IActionResult> GetStudentsForTeacher(string teacherId)
        {
            try
            {
                var students = await _teacherMangmentService.GetStudentsForTeacher(teacherId);
                return Ok(students);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
