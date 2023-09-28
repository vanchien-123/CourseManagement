using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class Instructor : BaseEnity
    {
        public string? TaxCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ParttimeSubject { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Schedule> TimeTables { get; set; }
    }

  
}
