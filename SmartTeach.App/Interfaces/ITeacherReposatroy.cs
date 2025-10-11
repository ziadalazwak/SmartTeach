using SmartTeach.App.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Interfaces
{
    public interface ITeacherReposatroy
    {
        public Task<TeacherRequestForParentInfo> GetTeacherInfoForParentAsync(string teacherId);
    }
}
