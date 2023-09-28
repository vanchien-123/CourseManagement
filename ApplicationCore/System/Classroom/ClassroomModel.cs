using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Classroom
{
    public class ClassroomModel
    {
        public string Name { get; set; }
        public string Depscription { get; set; }
        public string SchoolYear { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? ScheduleId { get; set; }
        public int? AmountStudent { get; set; }
        public decimal? Tuition { get; set; }
        public string Avatar { get; set; }
        public decimal? TuitionFee { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
