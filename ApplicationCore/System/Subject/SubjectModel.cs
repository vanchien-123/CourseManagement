using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Subject
{
    public class SubjectModel
    {
        public string Name { get; set; }
        public Guid? CombinationId { get; set; }
        //public Combination Combination { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? InstructorId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
