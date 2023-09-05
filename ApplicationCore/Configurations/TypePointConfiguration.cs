using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationCore.Configurations
{

    public class TypePointConfiguration : IEntityTypeConfiguration<TypePoint>
    {
        public void Configure(EntityTypeBuilder<TypePoint> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
