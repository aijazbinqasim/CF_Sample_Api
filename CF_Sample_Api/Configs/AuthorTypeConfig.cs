using CF_Sample_Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CF_Sample_Api.Configs
{
    public class AuthorTypeConfig : IEntityTypeConfiguration<AuthorModel>
    {
        public void Configure(EntityTypeBuilder<AuthorModel> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.LastName)
                .HasMaxLength(250);

            builder.Property(x => x.Email)
                .HasMaxLength(250);

            builder.HasData(
                new AuthorModel
                {
                    Id = 1,
                    FirstName = "Aijaz",
                    LastName = "Ali",
                    Email = "aijaz.ali@hotmail.com"
                }
            );
        }
    }
}
