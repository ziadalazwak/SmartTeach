using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.App.Dto.StudentGroup;
using SmartTeach.App.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartTeach.App.Dto.ParentInfoDto
{
    public class StudentInfoForParentDto
    {
        public int Id { get; set; } 
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public IEnumerable<ParentStudentGroupInfoDto>? Group { get; set; }
       public TeacherRequestForParentInfo? TeacherInfo { get; set; }
        public IEnumerable<ParentAttendaceInfoDto>? Attendance { get; set; }
       

    }
}
