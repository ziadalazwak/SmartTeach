using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.App.Dto.ParentInfoDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.SessionDto
{
    public class ParentSessionInfoDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public DateTime? StartTime { get; set; }
        public string? Topic { get; set; }

        public IEnumerable<ParentAttendaceInfoDto>? Attendances { get; set; }

    }
}
