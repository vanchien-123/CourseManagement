using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class CourseException: Exception
    {
        public CourseException() { }
        public CourseException(string message) : base(message) { }
        public CourseException(string message, Exception innerException) : base(message, innerException) { }

    }
}
