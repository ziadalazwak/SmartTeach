using SmartTeach.App.Dto.AttendanceDto;
using SmartTeach.App.Dto.ParentInfoDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Mapping.ParentInfoMapping
{
    public static class AttendaceInfoMap
    {
        public static ParentAttendaceInfoDto MapToParentDto(this Attendance attendance)
        {
            if (attendance==null) return null;
            return new ParentAttendaceInfoDto
            {
                Id=attendance.Id,
      
           
                Session=attendance.Session.MapToParentDto(),
       
            };

        }
        public static IEnumerable<ParentAttendaceInfoDto> MapToParentDtos(this IEnumerable<Attendance> attendances)
        {
            var getAttendaces = attendances.Select(a =>
            new ParentAttendaceInfoDto
            {
                Id=a.Id,
            



                
                IsPresent=a.IsPresent,
            }
            );

            return getAttendaces;
        }
    }
}
