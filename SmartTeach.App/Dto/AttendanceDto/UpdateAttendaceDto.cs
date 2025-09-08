using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.AttendanceDto
{
    public class UpdateAttendaceDto
    {
        public int StudentId { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsPresent { get; set; } = true;
    }
}
