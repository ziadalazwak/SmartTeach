using SmartTeach.App.Dto.SessionDto;
using SmartTeach.App.Dto.StudentDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.AttendanceDto
{
    public class GetAttendaceDto
    {
             public int Id { get; set; }
        public int StudentId { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public GetStudentDto? Student { get; set; }
        public GetSessionDto? Session { get; set; }
    }
}
