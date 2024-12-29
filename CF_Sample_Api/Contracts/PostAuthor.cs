namespace CF_Sample_Api.Contracts
{
    public class PostAuthor
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
    public class PostAuthorValidator : AbstractValidator<PostAuthor>
    {
        public PostAuthorValidator()
        {
            RuleFor(model => model.FirstName)
                .NotEmpty();

            RuleFor(model => model.Email)
                .EmailAddress();
        }
    }
}
