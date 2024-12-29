namespace CF_Sample_Api.Endpoints.Account
{
    public static class AuthEndpoint
    {
        public static IEndpointRouteBuilder MapAuthEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/account/register", async ([FromBody] PostAppUser postAppUser, IAuthService authService,
                IValidator<PostAppUser> validator) =>
            {
                var response = new ApiResponse<(bool Succeeded, string message, List<string> Errors)>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false
                };

                var vResult = validator.Validate(postAppUser);
                if (!vResult.IsValid)
                {
                    response.ErrorMsgs = vResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return Results.BadRequest(response);
                }

                var (Succeeded, message, Errors) = await authService.CreateUserAsync(postAppUser);

                if (!Succeeded)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        response.StatusCode = HttpStatusCode.Conflict;
                        response.Message = message;
                    }
                    else
                        response.ErrorMsgs = Errors;

                    return Results.BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Message = message;

                return Results.Created(string.Empty, response);
            });

            return app;
        }
    }
}
