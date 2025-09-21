using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.SessionDto
{
    public class SessionRequestQuery
    {
        public int? GroupId { get; set; }   
        public int? Year { get; set; }   
        public int? Month { get; set; }  
        public int? Day { get; set; }

    }
}
