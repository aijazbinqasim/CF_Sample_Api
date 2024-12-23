﻿namespace CF_Sample_Api.Contracts
{
    public class PutAuthor
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }

    public class PutAuthorValidator : AbstractValidator<PutAuthor>
    {
        public PutAuthorValidator()
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
