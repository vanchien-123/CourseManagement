using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Tuition
{
    public class TuitionModel
    {
        public Guid? StudentId { get; set; }
        public Guid? ClassroomId { get; set; }
        public string TypeTuition { get; set; }
        public decimal? FeeLevel { get; set; }
        public int? Discount { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
