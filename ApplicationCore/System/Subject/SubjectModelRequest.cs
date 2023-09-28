using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Subject
{
    public class SubjectModelRequest
    {
        public SubjectModelRequest()
        {
            this.SearchText = string.Empty;
        }
        public string SearchText { get; set; } 
        public Guid? CambinationId { get; set; }
        public Guid? CourseId { get; set; } 
    }
}
