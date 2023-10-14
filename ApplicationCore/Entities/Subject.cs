namespace ApplicationCore.Entities
{
    public class Subject : BaseEnity
    {
        public string Name { get; set; }
        public Guid? CombinationId { get; set; }
        public Combination Combination { get; set; }
        public Guid? CourseId { get; set; }
        public Course Course { get; set; }
        public Guid? InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<Point> Points { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
