namespace ApplicationCore.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public Guid ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Tuition> Tuitions { get; set; }
    }
}
