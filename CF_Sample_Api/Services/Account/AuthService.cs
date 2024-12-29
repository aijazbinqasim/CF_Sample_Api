namespace CF_Sample_Api.Services.Account
{
    public class AuthService(UserManager<AppUserModel> userManager, IMapper mapper, ILogger<AuthService> logger) : IAuthService
    {
        private readonly UserManager<AppUserModel> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthService> _logger = logger;

        public async Task<(bool Succeeded, string message, List<string> Errors)> CreateUserAsync(PostAppUser postAppUser)
        {
            try
            {
                if (await _userManager.FindByNameAsync(postAppUser.UserName!) != null)
                    return (false, "Username already exists", new List<string>());

                var result = await _userManager.CreateAsync(_mapper.Map<AppUserModel>(postAppUser), postAppUser.Password!);

                if (!result.Succeeded)
                    return (false, string.Empty, result.Errors.Select(e => e.Description).ToList());

                return (true, "User created successfully.", new List<string>());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating user {UserName}: {Message}", postAppUser.UserName, ex.Message);
                throw;
            }
        }
    }
}
