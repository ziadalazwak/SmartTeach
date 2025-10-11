using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.StudentAttendaceDto
{
    public class StudentForAttendaceDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
     public bool IsPresent { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
