namespace CF_Sample_Api.Services.Account
{
    public class AuthService(UserManager<AppUserModel> userManager, IMapper mapper, ILogger<AuthService> logger, IConfiguration configuration) : IAuthService
    {
        private readonly UserManager<AppUserModel> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IConfiguration _configuration = configuration;

        public async Task<(bool Succeeded, string message, List<string> Errors)> CreateUserAsync(PostAppUser user)
        {
            try
            {
                if (await _userManager.FindByNameAsync(user.UserName!) != null)
                    return (false, "Username already exists", new List<string>());

                var result = await _userManager.CreateAsync(_mapper.Map<AppUserModel>(user), user.Password!);

                if (!result.Succeeded)
                    return (false, string.Empty, result.Errors.Select(e => e.Description).ToList());

                return (true, "User created successfully.", new List<string>());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating user {UserName}: {Message}", user.UserName, ex.Message);
                throw;
            }
        }
        public async Task<(bool Succeeded, string message, List<string> Errors, object? userInfo)> SignInAsync(GetAppUser user)
        {
            try
            {
                var errors = new List<string>();

                var appUser = await _userManager.FindByNameAsync(user.UserName!);
                if (appUser == null)
                    errors.Add("Invalid username");

                else if (!await _userManager.CheckPasswordAsync(appUser!, user.Password!))
                    errors.Add("Invalid password");

                if (errors.Count != 0)
                    return (false, "User unauthorized", errors, null);
                
                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, appUser!.Id),
                    new(ClaimTypes.Name, appUser.UserName!),
                    new(ClaimTypes.Email, appUser.Email!),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = GenerateJwt(authClaims);
                var userInfo = new
                {
                    UserID = appUser.Id,
                    Username = appUser.UserName,
                    Useremail = appUser.Email,
                    Usertoken = token
                };

                return (true, "User authorized", new List<string>(), userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error signing in user {UserName}: {Message}", user.UserName, ex.Message);
                throw;
            }
        }
        private string GenerateJwt(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CipherHelper.Decrypt(_configuration["Jwt:Key"]!)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration.GetValue<string>("Jwt:Audience"),
                Issuer = _configuration.GetValue<string>("Jwt:Issuer"),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
