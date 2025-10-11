using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartTeach.App.Interfaces;
using SmartTeach.App.Request;
using SmartTeach.Persistence.Dbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Domain.Interfaces
{
    public class TeacherReposatory : ITeacherReposatroy
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public TeacherReposatory(UserManager<ApplicationUser> userManager) {
        _userManager = userManager;
        }
        public async Task<TeacherRequestForParentInfo> GetTeacherInfoForParentAsync(string teacherId)
        {
         var teacher =  await _userManager.Users.FirstOrDefaultAsync(t => t.Id == teacherId);
            if (teacher == null)
                throw new InvalidOperationException("Teacher not found.");
            var teacherInfo = new TeacherRequestForParentInfo
            {
                TeacherFullName = $"{teacher.FirstName} {teacher.LastName}",
                TeacherEmail = teacher.Email,
                TeacherPhoneNumber = teacher.PhoneNumber,
                // Add other properties as needed
            };
            return teacherInfo;
        }
    }
}
