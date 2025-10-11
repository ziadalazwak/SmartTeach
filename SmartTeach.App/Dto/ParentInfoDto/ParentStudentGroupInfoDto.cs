using SmartTeach.App.Dto.StudentDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.StudentGroup
{
    public class ParentStudentGroupInfoDto
    {
        public int StudentId { get; set; }

        public int GroupId { get; set; }


        public DateTime? EnrollmentDate { get; set; } = DateTime.MinValue;



        public GetStudentDto Student { get; set; }


        public ParentGroupInfoDto Group { get; set; }
    }
}
