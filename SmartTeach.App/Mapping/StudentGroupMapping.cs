using SmartTeach.App.Dto.StudentGroupDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Mapping
{
    public static class StudentGroupMapping
    {
        public static StudentGroup MapToDomain(this AddStudentToGroupDto dto)
        {
            return new StudentGroup
            {
                StudentId = dto.StudentId,
                GroupId = dto.GroupId,
            
           
            };

        }
        public static StudentGroupDto MapToDto(this StudentGroup studentGroup)
        {
            return new StudentGroupDto
            {
                StudentId = studentGroup.StudentId,
                GroupId = studentGroup.GroupId,
                EnrollmentDate = studentGroup.EnrollmentDate,
                Student = studentGroup.Student.MapToGetStudentDto(),
                Group = studentGroup.Group.MapToGroupDto()
            };
        }
        public static IEnumerable<StudentGroupDto> MapToDtoList(this IEnumerable<StudentGroup> studentGroups)
        {
            return studentGroups.Select(a=>a.MapToDto()).ToList();
        }
        public static List<StudentGroup> MapToDomainList(this IEnumerable<AddStudentToGroupDto> dtos)
        {
            return dtos.Select(MapToDomain).ToList();
        }
        
    }
}
