using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Holiday
{
    public class HolidayModel
    {
        public string Name { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
