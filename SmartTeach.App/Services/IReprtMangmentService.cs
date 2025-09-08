using SmartTeach.App.Dto.StudentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Services
{
    public interface IReprotMangmentService
    {
        public Task<byte[] > GenerateStudentReportAsync(int studentId);
        public Task<byte[]> GenerateSessionReportAsync(int sessionId);
        public Task<IEnumerable<GetStudentDtoforReport>> GetStudentDetailsForReportAsync(string studentName);

    }
}
