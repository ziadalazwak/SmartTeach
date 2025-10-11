using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Domain.Models
{

    public class Student
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public int Id { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public  ICollection<StudentGroup>? StudentGroups { get; set; } 
        public string Address { get; set; }
        [Phone]

        public string PhoneNumber { get; set; }

    }
}
