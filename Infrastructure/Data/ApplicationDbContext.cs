using ApplicationCore.Configurations;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infeastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Combination> Combination { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<Point> Point { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<TypePoint> TypePoint { get; set; }
        public DbSet<Schedule> TimeTable { get; set; }
        public DbSet<Tuition> Tuition { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configuration relation table
            builder.ApplyConfiguration(new ClassroomConfiguration());
            builder.ApplyConfiguration(new ClassroomScheduleRelConfiguration());
            builder.ApplyConfiguration(new PointConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new InstructorConfiguration());
            builder.ApplyConfiguration(new TypePointConfiguration());
            builder.ApplyConfiguration(new HolidayConfiguration());
            builder.ApplyConfiguration(new SubjectConfiguration());
            builder.ApplyConfiguration(new CombinationConfiguration());
            builder.ApplyConfiguration(new ScheduleConfiguration());
            builder.ApplyConfiguration(new TuitionConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            

            base.OnModelCreating(builder);

            //Data seed table Qualification
            builder.Entity<TypePoint>().HasData(new TypePoint { Id = "1", Name = " Kiểm tra miệng", Coefficient = 1 });
            builder.Entity<TypePoint>().HasData(new TypePoint { Id = "2", Name = " Kiểm tra 15p", Coefficient = 1 });
            builder.Entity<TypePoint>().HasData(new TypePoint { Id = "3", Name = " Kiểm tra 1 tiết", Coefficient = 2 });
            builder.Entity<TypePoint>().HasData(new TypePoint { Id = "4", Name = " Kiểm tra cuối kì", Coefficient = 3 });
            

            




        }
    }

}