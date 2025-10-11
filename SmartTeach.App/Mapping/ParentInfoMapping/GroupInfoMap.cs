using SmartTeach.App.Dto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Mapping.ParentInfoMapping
{
    public static class GroupInfoMap
    {
        public static ParentGroupInfoDto MapToParentDto(this Group group)
        {
            if (group == null) return null;

            return new ParentGroupInfoDto
            {
                Id = group.Id,
                Name = group.Name,
                Subject = group.Subject,
                centerd = group.centerd,
           

            };

        }
        public static IEnumerable<ParentGroupInfoDto> MapToParentDtos(this IEnumerable<Group> groups)
        {
            if (groups == null) return null;
            return groups.Select(g => new ParentGroupInfoDto
            {
                Id = g.Id,
                Name = g.Name,
                Subject = g.Subject,
                centerd = g.centerd,

            });
        }
    }
}
