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
                .NotEmpty()
                .NotNull()
                .WithMessage("First Name is required");

            RuleFor(model => model.Email)
                .EmailAddress()
                .WithMessage("Email is not valid");
        }
    }
}
