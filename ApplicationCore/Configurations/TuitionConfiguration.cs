using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationCore.Configurations
{
    public class TuitionConfiguration : IEntityTypeConfiguration<Tuition>
    {
        public void Configure(EntityTypeBuilder<Tuition> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TypeTuition).IsRequired();

            builder.HasOne(x=>x.Student)
                .WithMany(x=>x.Tuitions)
                .HasForeignKey(x=>x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Classroom)
                .WithMany()
                .HasForeignKey(x => x.ClassroomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
