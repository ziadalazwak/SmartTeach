using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Domain.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int GroupId { get; set; }    
        public DateTime StartTime { get; set; }
        
        public string? Topic { get; set; }
        public Group Group { get; set; } 
        public  ICollection<Attendance>? Attendances { get; set; } 

    }
}
