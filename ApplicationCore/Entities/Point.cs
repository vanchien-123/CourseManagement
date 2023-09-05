namespace ApplicationCore.Entities
{
    public class Point
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string TypePointId { get; set; }
        public TypePoint TypePoint { get; set; }
        public int PointCol { get; set; }
        public int PointColRequired { get; set; }

    }
}
