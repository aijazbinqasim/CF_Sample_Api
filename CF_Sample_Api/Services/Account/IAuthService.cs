namespace CF_Sample_Api.Services.Account
{
    public interface IAuthService
    {
        Task<(bool Succeeded, string message, List<string> Errors, object? userInfo)> CreateUserAsync(PostAppUser user);
        Task<(bool Succeeded, string message, List<string> Errors, object? userInfo)> SignInAsync(GetAppUser user);
    }
}
