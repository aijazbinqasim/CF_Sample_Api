using CF_Sample_Api.Contracts;
using CF_Sample_Api.Interfaces;

namespace CF_Sample_Api.Endpoints
{
    public static class Endpoint
    {
        public static IEndpointRouteBuilder MapEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/authors", async (PostAuthor postAuthor, IAuthorService authorService) =>
            {
                var author = await authorService.SaveAuthorAsync(postAuthor);
                return Results.Created($"/api/authors/{author.Id}", author);
            });

            app.MapGet("/authors", async (IAuthorService authorService) =>
            {
                var authors = await authorService.GetAuthorsAsync();
                return Results.Ok(authors);
            });

            app.MapGet("/authors/{id:long}", async (long id, IAuthorService authorService) =>
            {
                var author = await authorService.GetAuthorByIdAsync(id);
                return author != null ? Results.Ok(author) : Results.NotFound();
            });

            app.MapPut("/authors/{id:long}", async (long id, PutAuthor putAuthor, IAuthorService authorService) =>
            {
                var author = await authorService.UpdateAuthorAsync(id, putAuthor);
                return author != null? Results.Ok(author) : Results.NotFound();
            });

            app.MapDelete("/authors/{id:long}", async (long id, IAuthorService authorService) =>
            {
                var isDeleted = await authorService.DeleteAuthorAsync(id);
                return isDeleted? Results.NoContent() : Results.NotFound();
            });

            return app;
        }
    }
}
