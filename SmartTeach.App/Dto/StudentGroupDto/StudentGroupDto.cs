using SmartTeach.App.Dto.StudentDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.StudentGroupDto
{
    public class StudentGroupDto
    {
        public int StudentId { get; set; }

        public int GroupId { get; set; }


        public DateTime? EnrollmentDate { get; set; } = DateTime.Now;



        public GetStudentDto Student { get; set; }


        public GroupDto Group { get; set; }
    }
}
