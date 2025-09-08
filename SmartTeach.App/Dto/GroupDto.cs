using SmartTeach.App.Dto.StudentGroupDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }

        public string centerd { get; set; }
        public ICollection<Session>? Sessions { get; set; }
        public IEnumerable<StudentGroupDto.StudentGroupDto>? GroupStudents { get; set; }
    }
}
