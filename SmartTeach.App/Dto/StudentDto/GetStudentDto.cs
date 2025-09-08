using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.App.Dto.StudentGroupDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.StudentDto
{
    public class GetStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public ICollection<GetAttendaceDto>? Attendances { get; set; }
        public ICollection<Payment>? Payments { get; set; }
 
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

    }
}
