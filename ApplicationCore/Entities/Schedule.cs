using ApplicationCore.Entities;
using System.Collections.ObjectModel;

namespace ApplicationCore.Entities
{
    public class Schedule : BaseEnity
    {
        public Guid? SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string Room { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public string Day { get; set; }
        public Guid? InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<ClassroomScheduleRel> ClassroomScheduleRels { get; set; }
    }
}
