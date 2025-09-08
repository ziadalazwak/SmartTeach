using SmartTeach.App.Interfaces;
using SmartTeach.Domain.Interfaces;
using SmartTeach.Domain.Models;
using SmartTeach.Persistence.Dbcontext;
using SmartTeach.Persistence.Reposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly SmartTeachDbcontext _context;
        public UnitOfWork(SmartTeachDbcontext context)
        {
            _context=context;
            Students=new GenericReposatory<Student>(_context);
            Groups=new GenericReposatory<Group>(_context);
            Sessions=new SessionReposatory(_context);
            Attendances=new AttendacesReposatory(_context);
            StudentsGroups=new GenericReposatory<StudentGroup>(_context);
            Payments=new GenericReposatory<Payment>(_context);
           
        }
      public  IGenericReposatory<Student> Students { get; }
        public IGenericReposatory<Group> Groups { get; }
        public ISessionReposatory Sessions { get; }
        public IGenericReposatory<StudentGroup> StudentsGroups { get; }
        public IGenericReposatory<Payment> Payments { get; }
        public IAttendacesReposatory Attendances { get; }
        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose() ;
        }
    }
}
