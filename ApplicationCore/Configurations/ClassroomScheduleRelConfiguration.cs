using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationCore.Configurations
{
    public class ClassroomScheduleRelConfiguration : IEntityTypeConfiguration<ClassroomScheduleRel>
    {
        public void Configure(EntityTypeBuilder<ClassroomScheduleRel> builder)
        {
            builder.HasKey(x=> new {x.ScheduleId, x.ClassroomId});

            builder.HasOne(x => x.Schedule)
               .WithMany(x => x.ClassroomScheduleRels)
               .HasForeignKey(x => x.ScheduleId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Classroom)
              .WithMany(x => x.ClassroomScheduleRels)
              .HasForeignKey(x => x.ClassroomId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
