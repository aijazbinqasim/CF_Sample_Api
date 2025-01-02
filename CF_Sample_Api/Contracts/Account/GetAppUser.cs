namespace CF_Sample_Api.Contracts.Account
{
    public class GetAppUser
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class GetAppUserValidator : AbstractValidator<GetAppUser>
    {
        public GetAppUserValidator()
        {
            RuleFor(model => model.UserName)
                .NotEmpty();
            RuleFor(model => model.Password)
                .NotEmpty();
        }
    }
}
