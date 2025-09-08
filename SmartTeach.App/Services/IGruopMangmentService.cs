using SmartTeach.App.Dto;
using SmartTeach.App.Dto.StudentDto;
using SmartTeach.App.Dto.StudentGroupDto;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public interface IGruopMangmentService
    {
        public  Task<AddGroupDto> AddGroup(AddGroupDto group);
        public Task<StudentGroupDto> AddStudentToGroup(AddStudenetDto student, int GroupId);
        public Task<IEnumerable<StudentGroupDto>> GetAllStudentGroups();
        public Task<IEnumerable<StudentGroupDto>> GetStudentGroupById(int id);
        public void DeleteStudent(int studentId);   
        public UpdateGroupDto UpdateGroup(UpdateGroupDto group);
        public void DeleteGroup(int id );
        public Task< GroupDto >GetGroupById(int id);
        public Task<IEnumerable<GroupDto>> GetAllGroups();
    }
}
