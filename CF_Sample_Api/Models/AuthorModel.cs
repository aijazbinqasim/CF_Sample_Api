namespace CF_Sample_Api.Models
{
    public class AuthorModel
    {
        public long Id { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
