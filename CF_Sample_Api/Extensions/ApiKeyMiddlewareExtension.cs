namespace CF_Sample_Api.Extensions
{
    public static class ApiKeyMiddlewareExtension
    {
        public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<ApiKeyMiddleware>();
    }
}
