using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Domain.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
      public string? ApplicationUserId { get; set; }    
        public string Subject { get; set; }

        public string centerd { get; set; }
        public   ICollection<Session>? Sessions { get; set; }
        public ICollection<StudentGroup>? GroupStudents { get; set; }

    }
}
