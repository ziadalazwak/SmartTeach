using SmartTeach.App.Dto.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.AttendanceDto
{
    public class SessionAttendanceDisplayDto
    {
        public int SessionId { get; set; }
        public string SessionTopic { get; set; }
        public DateTime SessionStartTime { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<StudentAttendanceDto> Students { get; set; } = new List<StudentAttendanceDto>();
        public int TotalStudents { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
    }

    public class StudentAttendanceDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public bool IsPresent { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public bool HasAttendanceRecord { get; set; } // Indicates if attendance was actually marked
    }
}

