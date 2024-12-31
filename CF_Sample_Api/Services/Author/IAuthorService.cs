namespace CF_Sample_Api.Services.Author
{
    public interface IAuthorService
    {
        Task<GetAuthor> SaveAuthorAsync(PostAuthor postAuthor);
        Task<IEnumerable<GetAuthor>> GetAuthorsAsync();
        Task<GetAuthor> GetAuthorByIdAsync(long id);
        Task<GetAuthor> UpdateAuthorAsync(long id, PutAuthor putAuthor);
        Task<bool> DeleteAuthorAsync(long id);
    }
}
