namespace CF_Sample_Api.Endpoints.Author
{
    public static class AuthorEndpoint
    {
        public static IEndpointRouteBuilder MapAuthorEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/authors", async ([FromBody] PostAuthor postAuthor, IAuthorService authorService,
                IValidator<PostAuthor> validator) =>
            {
                var response = new ApiResponse<GetAuthor>
                {
                    Data = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false
                };

                var vResult = validator.Validate(postAuthor);
                if (!vResult.IsValid)
                {
                    response.ErrorMsgs = vResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return Results.BadRequest(response);
                }

                var author = await authorService.SaveAuthorAsync(postAuthor);
                response.Data = author;
                response.StatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                return Results.Created($"/api/authors/{author.Id}", response);
            });

            app.MapGet("/authors", async (IAuthorService authorService) =>
            {
                var authors = await authorService.GetAuthorsAsync();
                var response = new ApiResponse<IEnumerable<GetAuthor>>
                {
                    Data = authors,
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true
                };
                return Results.Ok(response);
            });

            app.MapGet("/authors/{id:long}", async ([FromRoute] long id, IAuthorService authorService) =>
            {
                var author = await authorService.GetAuthorByIdAsync(id);
                var response = new ApiResponse<GetAuthor>
                {
                    Data = author,
                    StatusCode = author != null ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                    IsSuccess = author != null
                };
                return author != null ? Results.Ok(response) : Results.NotFound(response);
            });

            app.MapPut("/authors/{id:long}", async ([FromRoute] long id, PutAuthor putAuthor, IAuthorService authorService,
                IValidator<PutAuthor> validator) =>
            {
                var response = new ApiResponse<GetAuthor>
                {
                    Data = null,
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false
                };

                var vResult = validator.Validate(putAuthor);
                if (!vResult.IsValid)
                {
                    response.ErrorMsgs = vResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return Results.BadRequest(response);
                }

                var author = await authorService.UpdateAuthorAsync(id, putAuthor);
                response.Data = author;
                response.StatusCode = author != null ? HttpStatusCode.OK : HttpStatusCode.NotFound;
                response.IsSuccess = author != null;
                return author != null ? Results.Ok(response) : Results.NotFound(response);
            });

            app.MapDelete("/authors/{id:long}", async ([FromRoute] long id, IAuthorService authorService) =>
            {
                var isDeleted = await authorService.DeleteAuthorAsync(id);
                var response = new ApiResponse<bool>
                {
                    Data = isDeleted,
                    StatusCode = isDeleted ? HttpStatusCode.NoContent : HttpStatusCode.NotFound,
                    IsSuccess = isDeleted
                };
                return isDeleted ? Results.NoContent() : Results.NotFound(response);
            });

            return app;
        }
    }
}
