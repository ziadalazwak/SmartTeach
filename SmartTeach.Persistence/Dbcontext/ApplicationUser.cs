using Microsoft.AspNetCore.Identity;
using SmartTeach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Persistence.Dbcontext
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Group>? Groups { get; set; } 
    }
}
