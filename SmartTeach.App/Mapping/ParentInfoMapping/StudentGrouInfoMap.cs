using SmartTeach.App.Dto.StudentGroup;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Mapping.ParentInfoMapping
{
    public static class StudentGrouInfoMap
    {
        public static ParentStudentGroupInfoDto MapToParentDto(this StudentGroup studentGroup)
        {
            return new ParentStudentGroupInfoDto
            {
                EnrollmentDate = studentGroup.EnrollmentDate,
                Group = studentGroup.Group.MapToParentDto()
            };
        }
        public static IEnumerable<ParentStudentGroupInfoDto> MapToParentDtos(this IEnumerable<StudentGroup> studentGroups)
        {
            return studentGroups.Select(a => a.MapToParentDto()).ToList();
        }
  
    }
}
