using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Mapping
{
    public static class AttendaceMapping
    {
        public static Attendance MapToDomain(this AddAttendanceDto attendanceDto,int sessionid)
        {
            if (attendanceDto==null) return null;
           return  new Attendance
            {
                
                IsPresent = attendanceDto.IsPresent,
                StudentId=attendanceDto.StudentId,  
                
                SessionId=sessionid

            };
        }
        public static  IEnumerable <Attendance> MapToDomains(this IEnumerable< AddAttendanceDto> attendanceDto,int sessionid)
        {
            if (attendanceDto==null) return null;

           return attendanceDto.Select(a=>a.MapToDomain(sessionid)).ToList();

            return null;
        }
        public static Attendance MapToDomain(this UpdateAttendaceDto updateAttendaceDto)
        {
            if (updateAttendaceDto==null) return null;
            return new Attendance
            {
                IsPresent=updateAttendaceDto.IsPresent,
                StudentId=updateAttendaceDto.StudentId,
                SessionId=updateAttendaceDto.SessionId,
            };
            }
        public static GetAttendaceDto MapToDto(this Attendance attendance)
        {
            if (attendance==null) return null;
            return new GetAttendaceDto
            {
                Id=attendance.Id,
                Student=attendance.Student.MapToGetStudentDto()
                ,
                StudentId=attendance.StudentId,
                Session=attendance.Session.MapToDto(),
                SessionId=attendance.SessionId
            };
            
        }
        public static IEnumerable<GetAttendaceDto> MapTDtos(this IEnumerable<Attendance> attendances) 
        {
            var getAttendaces = attendances.Select(a =>
            new GetAttendaceDto
            {
                Id=a.Id,
                Student=a.Student.MapToGetStudentDto()
                
                
              
                ,
                IsPresent=a.IsPresent,
            }
            );

            return getAttendaces;
        }

    }
}
