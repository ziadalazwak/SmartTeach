using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.App.Dto.SessionDto
{
    public class UpdateSessionDto
    {

        public int GroupId { get; set; }
        public DateTime StartTime { get; set; }
        public string? Topic { get; set; }

    }
}
