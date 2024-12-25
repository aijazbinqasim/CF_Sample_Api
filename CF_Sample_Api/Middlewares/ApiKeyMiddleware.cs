namespace CF_Sample_Api.Middlewares
{
    public class ApiKeyMiddleware(RequestDelegate next, ApiKeyValidationConfig apiKeyValidationConfig)
    {
        private readonly RequestDelegate _next = next;
        private readonly ApiKeyValidationConfig _apiKeyValidationConfig = apiKeyValidationConfig;

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyConstant.Header, out var userKey) || string.IsNullOrWhiteSpace(userKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync("API Key is missing or empty.");
                return;
            }

            if (!_apiKeyValidationConfig.IsValid(userKey!))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Invalid API Key.");
                return;
            }

            await _next(context);
        }
    }
}
