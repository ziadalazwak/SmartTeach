﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTeach.Domain.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public Student Student { get; set; }
        public Session Session { get; set; }
    }
}
