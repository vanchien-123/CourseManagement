namespace ApplicationCore.Entities
{
    public class Course : BaseEnity
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string StatusTuition { get; set; }
        public DateTime StartDay { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Point> Points { get; set; }
    }
}
