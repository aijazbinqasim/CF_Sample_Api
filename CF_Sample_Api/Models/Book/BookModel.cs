namespace CF_Sample_Api.Models.Book
{
    public class BookModel
    {
        public long BookId { get; set; }
        public required string BookTitle { get; set; }
        public required decimal BookPrice { get; set; }
        public string? Isbn { get; set; }
        public long? AuthorId { get; set; }
        public AuthorModel? Author { get; set; }
    }
}
