
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SmartTeach.App.Dto;
using SmartTeach.App.Dto.StudentDto;
using SmartTeach.App.Services;



namespace SmartTeach.Api.Controllers
{
  
    [ApiController]

    [Route("api/[controller]")]

    public class GroupMangmentCotroller:ControllerBase
    {
        private readonly IGruopMangmentService gruopMangment;
        public GroupMangmentCotroller(IGruopMangmentService gruopMangment)
        {
            this.gruopMangment=gruopMangment;
        }
        [HttpGet]
        public async Task<IActionResult> Get() { 
        var groups=await gruopMangment.GetAllGroups();
            return Ok ( groups );
        }
        [HttpPost]
        public async Task<IActionResult>Add(AddGroupDto group)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var add =await gruopMangment.AddGroup(group);

            return Ok ( add );
        }
        [HttpPost("AddStudent/{GroupId}")]
        public async Task<IActionResult>Addstudent(AddStudenetDto student, [FromRoute]int GroupId)
        {
            var add = await gruopMangment.AddStudentToGroup(student,GroupId);
            return Ok(add);
        }

        
        [HttpGet("StudentGroups/{id}")]
        public async Task<IActionResult> GetStudentGroups(int id )
        {
            var groups = await gruopMangment.GetStudentGroupById(id);
            return Ok(groups);
        }
        [HttpDelete("DeleteStudent/{studentId}")]
        public  IActionResult DeleteStudent(int studentId)
        {
            gruopMangment.DeleteStudent(studentId);
            return Ok ("DELETED");
        }
        [HttpDelete("DeleteGroup/{id}")]    
        public IActionResult DeleteGroup(int id)
        {
            gruopMangment.DeleteGroup(id);
            return Ok("Deleted");
        }
       
        
    }
}
