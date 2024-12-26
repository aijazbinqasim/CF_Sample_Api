namespace CF_Sample_Api.Configs
{
    public class AuthorModelTypeConfig : IEntityTypeConfiguration<AuthorModel>
    {
        public void Configure(EntityTypeBuilder<AuthorModel> builder)
        {
            builder.ToTable("Author");

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

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId);

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
