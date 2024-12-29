namespace CF_Sample_Api.Services.Account
{
    public class AuthService(UserManager<AppUserModel> userManager, IMapper mapper, ILogger<AuthService> logger) : IAuthService
    {
        private readonly UserManager<AppUserModel> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthService> _logger = logger;

        public async Task<(bool Succeeded, List<string> Errors)> CreateUserAsync(PostAppUser postAppUser)
        {
            try
            {
                if (await _userManager.FindByNameAsync(postAppUser.UserName!) != null)
                    return (false, new List<string> { "Username already exists." });

                var result = await _userManager.CreateAsync(_mapper.Map<AppUserModel>(postAppUser), postAppUser.Password!);

                if (result.Succeeded)
                    return (true, new List<string> { "User created successfully." });

                return (false, result.Errors.Select(e => e.Description).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating user {UserName}: {Message}", postAppUser.UserName, ex.Message);
                throw;
            }
        }
    }
}
