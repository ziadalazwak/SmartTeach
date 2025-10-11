using SmartTeach.App.Dto.SessionDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Mapping.ParentInfoMapping
{
    public static class SessionInfoMap
    {
        public static ParentSessionInfoDto MapToParentDto(this Session session)
        {
            if (session == null) return null;
            return new ParentSessionInfoDto
            {
                Id= session.Id,
                Topic = session.Topic,
                StartTime = session.StartTime,
              
            };

        }
        public static IEnumerable<ParentSessionInfoDto> MapToParentDtos(this IEnumerable<Session> sessions)
        {
            var sessionDtos = sessions.Select(s => s.MapToParentDto()).ToList();
            return sessionDtos;
        }
    }
}
