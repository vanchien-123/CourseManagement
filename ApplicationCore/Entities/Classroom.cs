namespace ApplicationCore.Entities
{
    public class Classroom : BaseEnity
    {
        public string Name { get; set; }
        public string Depscription { get; set; }
        public string SchoolYear { get; set; }
        public Guid? CourseId { get; set; }
        public Course Course { get; set; }
        public int? AmountStudent { get; set; }
        public decimal? Tuition { get; set; }
        public string Avatar { get; set; }
        public decimal? TuitionFee { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<ClassroomScheduleRel> ClassroomScheduleRels { get; set; }
        public ICollection<Subject> Subjects { get; set; }

    }
}
