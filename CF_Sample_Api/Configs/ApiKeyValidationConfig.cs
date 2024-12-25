namespace CF_Sample_Api.Configs
{
    public class ApiKeyValidationConfig(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public bool IsValid(string userKey)
        {
            if (string.IsNullOrWhiteSpace(userKey))
                return false;

            var apiKey = CipherHelper.Decrypt(_configuration.GetValue<string>($"Security:Api:{ApiKeyConstant.Key}")!);

            if (string.IsNullOrEmpty(apiKey) || apiKey != userKey)
                return false;

            return true;
        }
    }
}
