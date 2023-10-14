using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Classroom
{
    public class ClassroomDisplayModel
    {
        public string Name { get; set; }
        public string Depscription { get; set; }
        public string SchoolYear { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? ScheduleId { get; set; }
    }
}
