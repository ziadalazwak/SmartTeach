using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Request
{
    public class TeacherRequestForParentInfo
    {
        public string TeacherFullName { get; set; } = string.Empty; 
        public string TeacherEmail { get; set; } = string.Empty;
        public string TeacherPhoneNumber { get; set; } = string.Empty;

    }
}
