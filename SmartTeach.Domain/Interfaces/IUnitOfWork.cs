using SmartTeach.App.Interfaces;
using SmartTeach.Domain.Interfaces;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
    IGenericReposatory<Student> Students { get; }
        IGenericReposatory<Group> Groups{ get; }
     ISessionReposatory Sessions { get; }
    IGenericReposatory<StudentGroup>StudentsGroups{ get; }
        IGenericReposatory<Payment> Payments { get; }
        IAttendacesReposatory Attendances { get; }
        public Task<int> CompleteAsync();

    }
}