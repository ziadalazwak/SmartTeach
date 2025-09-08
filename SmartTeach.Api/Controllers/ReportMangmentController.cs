using Microsoft.AspNetCore.Mvc;
using SmartTeach.App.Services;

namespace SmartTeach.Api.Controllers
{
   [ApiController]
    [Route("api/[controller]")] 
    public class ReportMangmentController:ControllerBase
    {
        private readonly IReprotMangmentService _reportservice;
        public ReportMangmentController(IReprotMangmentService reprotservice)
        {
            _reportservice = reprotservice;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GenerateStudentReport(int id)
        {
            var reportBytes = await _reportservice.GenerateStudentReportAsync(id);
            if (reportBytes == null || reportBytes.Length == 0)
                return NotFound($"No report found for student ID {id}.");
            // Return the report as a downloadable file (assuming it's a text file for simplicity)
            return File(reportBytes, "application/octet-stream", $"StudentReport_{id}.txt");
        }   
        [HttpGet("session/{id}")]
        public async Task<IActionResult> GenerateSessionReport(int id)
        {
            var reportBytes = await _reportservice.GenerateSessionReportAsync(id);
            if (reportBytes == null || reportBytes.Length == 0)
                return NotFound($"No report found for session ID {id}.");
            // Return the report as a downloadable file (assuming it's a text file for simplicity)
            return File(reportBytes, "application/octet-stream", $"SessionReport_{id}.txt");
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetStudentDetailsForReport([FromQuery] string studentName)
        {
            var students = await _reportservice.GetStudentDetailsForReportAsync(studentName);
            if (students == null || !students.Any())
                return NotFound($"No students found matching the name '{studentName}'.");
            return Ok(students);
        }
    }
}
