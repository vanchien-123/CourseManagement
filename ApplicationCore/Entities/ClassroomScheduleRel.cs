namespace ApplicationCore.Entities
{
    public class ClassroomScheduleRel
    {
        public Guid ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
