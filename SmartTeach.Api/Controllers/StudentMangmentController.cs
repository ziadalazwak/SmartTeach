using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartTeach.App.Services;
using System.Security.Claims;

namespace SmartTeach.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentMangmentController: ControllerBase
    {
        private readonly IStudentMangmentService _studentmangmentservice;
        public StudentMangmentController(IStudentMangmentService studentmangmentservice)
        {
            _studentmangmentservice=studentmangmentservice;
        }
        [HttpGet("GetStudentsForTeacher")]
     
        public IActionResult GetStudentsForTeacher()
        {
            var teacherId = User.Claims.FirstOrDefault(c => c.Type==ClaimTypes.NameIdentifier)?.Value;
            try
            {
                var students = _studentmangmentservice.GetStudentsForTeacher(teacherId);
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
            [HttpGet("StudentInfoForParent/{studentId}")]
    
        public async Task<IActionResult> GetStudentInfoForParent(int studentId)
            {
                try
                {
                    var studentInfo = await _studentmangmentservice.GetStudentInfoForParent(studentId);
                    return Ok(studentInfo);
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

