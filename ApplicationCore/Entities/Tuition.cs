namespace ApplicationCore.Entities
{
    public class Tuition
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public string? TypeTuition { get; set; }
        public float FeeLevel { get; set; }
        public int? Discount { get; set; }
        public string? Note { get; set; }

    }
}
