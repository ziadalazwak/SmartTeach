using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.AttendanceDto
{
    public class AddAttendanceDto
    {

        public int StudentId { get; set; }
     
     
        public bool IsPresent { get; set; } = true;

    }
}
