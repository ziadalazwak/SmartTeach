using SmartTeach.App.Dto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Mapping
{
    public static class GroupMapping
    {
        public static Group MapToGroup(AddGroupDto group, string TeacherId)
        {
            if (group == null) return null;
            return new Group
            {
              ApplicationUserId=TeacherId,
                Name = group.Name,
                 Subject=group.Subject,
                 centerd=group.centerd,
               
            };
            
        }
        public static Group MapToGroup(UpdateGroupDto group)
        {
            if (group == null) return null;
            return new Group
            {
                
                Name = group.Name,
                Subject = group.Subject,
                centerd = group.centerd
            };
        }
        public static GroupDto MapToGroupDto(this Group group)
        {
            if (group == null) return null;
         
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Subject = group.Subject,
                centerd = group.centerd,
                Sessions = group.Sessions.MapToDtos(),
                
            };
            
        }
       public static IEnumerable<GroupDto> MapToGroupDtos(this IEnumerable<Group> groups)
        {
            if (groups == null) return null;
            return groups.Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.Name,
                Subject = g.Subject,
                centerd = g.centerd,
              
            });
        }   

    }
}
