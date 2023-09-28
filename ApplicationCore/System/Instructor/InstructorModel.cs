using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.System.Instructor
{
    public class InstructorModel
    {
        public string? TaxCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string? Avatar { get; set; }
        [NotMapped]
        public IFormFile fileAvatar { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ParttimeSubject { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
