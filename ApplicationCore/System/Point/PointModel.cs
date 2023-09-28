using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Point
{
    public class PointModel
    {
        public Guid? CourseId { get; set; }
        public Guid? SubjectId { get; set; }
        public Guid? TypePointId { get; set; }
        public int? PointCol { get; set; }
        public int? PointColRequired { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
