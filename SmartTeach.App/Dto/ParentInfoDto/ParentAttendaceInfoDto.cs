using SmartTeach.App.Dto.SessionDto;
using SmartTeach.App.Dto.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.ParentInfoDto
{
    public class ParentAttendaceInfoDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public GetStudentDto? Student { get; set; }
        public ParentSessionInfoDto? Session { get; set; }
    }
}
