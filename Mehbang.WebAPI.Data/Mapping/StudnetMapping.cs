using Mehbang.WebAPI.Data.Entities.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mehbang.WebAPI.Infrastructure.Mapping;

public class StudnetMapping : IEntityTypeConfiguration<Student>
{ 
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Studnts");
        builder.Property(x=>x.FullName);
        builder.Property(x=>x.NationalCode);
        builder.Property(x=>x.BirthDate);
    }
}
