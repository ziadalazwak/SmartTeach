using SmartTeach.App.Dto.SessionDto;
using SmartTeach.App.Dto.StudentGroup;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto
{
    public class ParentGroupInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }

        public string centerd { get; set; }
        public IEnumerable<ParentSessionInfoDto>? Sessions { get; set; }
        public IEnumerable<ParentStudentGroupInfoDto>? GroupStudents { get; set; }
    }
}
