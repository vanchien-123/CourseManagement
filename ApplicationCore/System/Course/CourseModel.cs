﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Course
{
    public class CourseModel
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string StatusTuition { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
