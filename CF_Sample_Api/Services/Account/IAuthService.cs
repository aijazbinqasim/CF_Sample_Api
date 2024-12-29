namespace CF_Sample_Api.Services.Account
{
    public interface IAuthService
    {            
        Task<(bool Succeeded, string message, List<string> Errors)> CreateUserAsync(PostAppUser postAppUser);
    }
}
