using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTeach.Domain.Models
{
    [Table("StudentGroups")]
    public class StudentGroup
    {
        
        public int StudentId { get; set; }

        public int GroupId { get; set; }

        
        public DateTime? EnrollmentDate { get; set; } = DateTime.Now;

        
      
        public  Student Student { get; set; }

       
        public  Group Group { get; set; }
    }
}
