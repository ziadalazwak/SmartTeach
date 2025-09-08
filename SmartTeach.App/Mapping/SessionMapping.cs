using SmartTeach.App.Dto.SessionDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Mapping
{
    public static class SessionMapping
    {
        public static GetSessionDto MapToDto(this Session session)
        {
            if (session == null) return null;
            return new GetSessionDto
            {
              Id= session.Id,
              Topic = session.Topic,
                StartTime = session.StartTime,
                Attendances= session.Attendances?.MapTDtos(),
               GroupId = session.GroupId
            };

        }
        public static IEnumerable<GetSessionDto> MapToDtos(this IEnumerable<Session> sessions)
        {
         var sessionDtos = sessions.Select(s => s.MapToDto()).ToList();
            return sessionDtos; 
        }
        public static Session MapToDomain(this AddSessionDto sessionDto,int groupid)
        {
            if (sessionDto == null) return null;
            return new Session
            {
                GroupId = groupid,
                StartTime = sessionDto.StartTime,
                Topic = sessionDto.Topic
            };
        }   
        public static Session MapToDomain(this UpdateSessionDto sessionDto)
        {
            if (sessionDto == null) return null;
            return new Session
            {
                
                GroupId = sessionDto.GroupId,
                StartTime = sessionDto.StartTime,
                Topic = sessionDto.Topic
            };
        }   

    }
}
