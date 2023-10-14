using ApplicationCore.System.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Student
{
    public class StudentDisplayModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }

        public IEnumerable<ClassroomDisplayModel> Classrooms { get; set; } = new List<ClassroomDisplayModel>();
    }
}
