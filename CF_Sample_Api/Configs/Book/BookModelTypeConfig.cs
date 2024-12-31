namespace CF_Sample_Api.Configs.Book
{
    public class BookModelTypeConfig : IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            builder.ToTable("Book");

            builder.HasKey(x => x.BookId);

            builder.Property(x => x.BookId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.BookTitle)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.BookPrice)
               .IsRequired()
               .HasPrecision(10, 2);

            builder.Property(x => x.Isbn)
                .HasMaxLength(250);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId);

            builder.HasData(
                new BookModel
                {
                    BookId = 1,
                    BookTitle = "Sample Book 1",
                    BookPrice = 100.0m,
                    AuthorId = 3
                },
                new BookModel
                {
                    BookId = 2,
                    BookTitle = "Sample Book 2",
                    BookPrice = 500.41m,
                    AuthorId = 3
                }
            );
        }
    }
}
