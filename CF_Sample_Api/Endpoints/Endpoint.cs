using CF_Sample_Api.Contracts;
using CF_Sample_Api.Interfaces;
using System.Net;

namespace CF_Sample_Api.Endpoints
{
    public static class Endpoint
    {
        public static IEndpointRouteBuilder MapEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/authors", async (PostAuthor postAuthor, IAuthorService authorService) =>
            {
                var author = await authorService.SaveAuthorAsync(postAuthor);
                var response = new ApiResponse<GetAuthor>
                {
                    Data = author,
                    StatusCode = HttpStatusCode.Created,
                    IsSuccess = true
                };
                return Results.Created($"/api/authors/{author.Id}", response);
            })
            .WithName("CreateAuthor")
            .Accepts<PostAuthor>("application/json")
            .Produces<ApiResponse<GetAuthor>>(201)
            .Produces<ApiResponse<string>>(400);

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
            })
            .WithName("GetAuthors")
            .Produces<ApiResponse<IEnumerable<GetAuthor>>>(200);

            app.MapGet("/authors/{id:long}", async (long id, IAuthorService authorService) =>
            {
                var author = await authorService.GetAuthorByIdAsync(id);
                var response = new ApiResponse<GetAuthor>
                {
                    Data = author,
                    StatusCode = author != null ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                    IsSuccess = author != null
                };
                return author != null ? Results.Ok(response) : Results.NotFound(response);
            })
            .WithName("GetAuthorById")
            .Produces<ApiResponse<GetAuthor>>(200)
            .Produces<ApiResponse<string>>(404);

            app.MapPut("/authors/{id:long}", async (long id, PutAuthor putAuthor, IAuthorService authorService) =>
            {
                var author = await authorService.UpdateAuthorAsync(id, putAuthor);
                var response = new ApiResponse<GetAuthor>
                {
                    Data = author,
                    StatusCode = author != null ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                    IsSuccess = author != null
                };
                return author != null ? Results.Ok(response) : Results.NotFound(response);
            })
            .WithName("UpdateAuthor")
            .Accepts<PutAuthor>("application/json")
            .Produces<ApiResponse<GetAuthor>>(200)
            .Produces<ApiResponse<string>>(404);

            app.MapDelete("/authors/{id:long}", async (long id, IAuthorService authorService) =>
            {
                var isDeleted = await authorService.DeleteAuthorAsync(id);
                var response = new ApiResponse<bool>
                {
                    Data = isDeleted,
                    StatusCode = isDeleted ? HttpStatusCode.NoContent : HttpStatusCode.NotFound,
                    IsSuccess = isDeleted
                };
                return isDeleted ? Results.NoContent() : Results.NotFound(response);
            })
            .WithName("DeleteAuthor")
            .Produces<ApiResponse<bool>>(204)
            .Produces<ApiResponse<string>>(404);
           
            return app;
        }
    }
}
