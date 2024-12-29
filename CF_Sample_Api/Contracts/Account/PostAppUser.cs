namespace CF_Sample_Api.Contracts.Account
{
    public class PostAppUser
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class PostAppUserValidator : AbstractValidator<PostAppUser>
    {
        public PostAppUserValidator()
        {
            RuleFor(model => model.UserName)
                .NotEmpty();

            RuleFor(model => model.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(model => model.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one non-alphanumeric character.");
        }
    }
}
