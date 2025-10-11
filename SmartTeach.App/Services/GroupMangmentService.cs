using SmartTeach.App.Dto;
using SmartTeach.App.Dto.StudentDto;
using SmartTeach.App.Interfaces;
using SmartTeach.Domain.Models;


using SmartTeach.App.Mapping;
using System.Threading.Tasks;
using SmartTeach.App.Dto.StudentGroup;
namespace SmartTeach.App.Services
{
    public class GroupMangmentService : IGruopMangmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroupMangmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public async Task<AddGroupDto> AddGroup(AddGroupDto group, string TeacherId)
        {
            var Domain= GroupMapping.MapToGroup(group,TeacherId); 


            await _unitOfWork.Groups.AddAsync(Domain);
          var add=  await _unitOfWork.CompleteAsync();
            
                return group;
           
        }
       

        public async Task<  StudentGroupDto> AddStudentToGroup(AddStudenetDto studentDto, int groupId)
        {
            var group = await _unitOfWork.Groups.GetByIdAsync(groupId);
            if (group == null)
                throw new ArgumentException($"Group with ID {groupId} does not exist.");
            
            var student = studentDto.MapToStudent();

            await _unitOfWork.Students.AddAsync(student);
            
            if (await _unitOfWork.CompleteAsync() <= 0)
                throw new InvalidOperationException("Could not create the student.");

            var studentGroup = new StudentGroup
            {
                StudentId = student.Id,
                GroupId = groupId
            };

            await _unitOfWork.StudentsGroups.AddAsync(studentGroup);
            if (await _unitOfWork.CompleteAsync() <= 0)
                throw new InvalidOperationException("Could not link the student to the group.");

            return new StudentGroupDto
            {
                StudentId = student.Id,
                GroupId = groupId
            };
        }

        public async Task AssignStudentToGroup(int studentId, int groupId)
        {
            var group = await _unitOfWork.Groups.GetByIdAsync(groupId);
            if (group == null)
                throw new ArgumentException($"Group with ID {groupId} does not exist.");
            var student = await _unitOfWork.Students.GetByIdAsync(studentId);
            if (student == null)
                throw new ArgumentException($"Student with ID {studentId} does not exist.");
            var studentGroup = new StudentGroup
            {
                StudentId = studentId,
                GroupId = groupId
            };
            await _unitOfWork.StudentsGroups.AddAsync(studentGroup);
            if (await _unitOfWork.CompleteAsync() <= 0)
                throw new InvalidOperationException("Could not link the student to the group.");
        }


        public void DeleteGroup(int id)
        {
            _unitOfWork.Groups.Delete(id);
            _unitOfWork.CompleteAsync();
        }

        public void DeleteStudent(int studentId)
        {
            _unitOfWork.Students.Delete(studentId);
            _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<GroupDto>> GetAllGroups(string TeacherId)
        {
            var groups=await _unitOfWork.Groups.GetAllAsync(filter:a=>a.ApplicationUserId==TeacherId);

            var mapgroup = groups.MapToGroupDtos();
            if (mapgroup!=null) return mapgroup;

            return null;

        }
        public async Task<IEnumerable<StudentGroupDto>>GetAllStudentGroups()
        {
            var Studentgroups = await _unitOfWork.StudentsGroups.GetAllAsync();

            var mapgroup = Studentgroups.MapToDtoList();

            if (mapgroup!=null) return mapgroup;

            return null;

        }
        public async Task<GroupDto> GetGroupById(int id)
        {
           var group =await  _unitOfWork.Groups.GetByIdAsync(id );


            if (group == null)
                throw new ArgumentException($"Group with ID {id} does not exist.");

     
            var groupDto = group.MapToGroupDto();

            return groupDto;
        }

        public async Task<IEnumerable<GetStudentDto>> GetStudentGroupById(int id)
        {
            var studentGroup = await _unitOfWork.StudentsGroups.GetAllAsync(filter: a => a.GroupId==id, includes: a => a.Student);
            if (studentGroup == null)
                throw new ArgumentException($"StudentGroup with ID {id} does not exist.");
            var students = studentGroup.Select(s => s.Student).ToList();
            return students.MapToGetStudentDtos();
        }
        public UpdateGroupDto UpdateGroup(UpdateGroupDto group)
        {
            _unitOfWork.Groups.Update(GroupMapping.MapToGroup(group));
           return group;    
        }
    }
}
