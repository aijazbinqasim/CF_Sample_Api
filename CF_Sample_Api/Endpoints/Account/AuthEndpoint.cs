namespace CF_Sample_Api.Endpoints.Account
{
    public static class AuthEndpoint
    {
        public static IEndpointRouteBuilder MapAuthEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/account/register", async ([FromBody] PostAppUser user, IAuthService authService,
                IValidator<PostAppUser> validator) =>
            {
                var response = new ApiResponse<object>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest
                };

                var vResult = validator.Validate(user);
                if (!vResult.IsValid)
                {
                    response.ErrorMsgs = vResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return Results.BadRequest(response);
                }

                var (Succeeded, message, Errors) = await authService.CreateUserAsync(user);
                if (!Succeeded)
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        response.StatusCode = HttpStatusCode.Conflict;
                        response.Message = message;
                    }
                    else response.ErrorMsgs = Errors;

                    return Results.BadRequest(response);
                }

                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Message = message;
                return Results.Created(string.Empty, response);
            });

            app.MapPost("/account/login", async ([FromBody] GetAppUser user, IAuthService authService,
                IValidator<GetAppUser> validator) =>
            {
                var response = new ApiResponse<object>
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest
                };

                var vResult = validator.Validate(user);
                if (!vResult.IsValid)
                {
                    response.ErrorMsgs = vResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return Results.BadRequest(response);
                }

                var (Succeeded, message, Errors, userInfo) = await authService.SignInAsync(user);
                if (!Succeeded)
                {
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.Message = message;
                    response.ErrorMsgs = Errors;
                    return Results.BadRequest(response);
                }

                response.Data = userInfo;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = message;
                return Results.Ok(response);
            });

            return app;
        }
    }
}
