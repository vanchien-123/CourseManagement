using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public bool IsRoot { get; set; }
        public bool IsDelete { get; set; }
    }
}
