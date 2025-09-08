using SmartTeach.App.Dto.StudentDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Mapping
{
    public static class StudentMapping
    {
        public static Student MapToStudent(this AddStudenetDto addStudentDto)
        {
            return new Student
            {
               Address = addStudentDto.Address,
           
                FirstName = addStudentDto.FirstName,
                LastName = addStudentDto.LastName,
                PhoneNumber = addStudentDto.PhoneNumber,
       
            };
        }
        public static IEnumerable<GetStudentDtoforReport> MapToGetStudentDtoforReport(this IEnumerable<Student> students)
        {
            var studentsforreport = students.Select(a => new GetStudentDtoforReport
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Address = a.Address,
                PhoneNumber = a.PhoneNumber,
            
            }).ToList();
            return studentsforreport;
        }
        public static GetStudentDto MapToGetStudentDto(this Student student)
        {
            return new GetStudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber
                ,
             
              
                Payments = student.Payments
            };
        }
        public static IEnumerable<GetStudentDto> MapToGetStudentDtos(this IEnumerable<Student> students)
        {
            return students.Select(s => s.MapToGetStudentDto()).ToList();
        }
        public static Student MapToStudent(this UpdateStudentDto updateStudentDto)
        {
            return new Student
            {
                Address = updateStudentDto.Address,

                FirstName = updateStudentDto.FirstName,
                LastName = updateStudentDto.LastName,
                PhoneNumber = updateStudentDto.PhoneNumber,

            };
        }
    }
}
