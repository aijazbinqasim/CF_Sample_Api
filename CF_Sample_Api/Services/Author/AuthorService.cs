namespace CF_Sample_Api.Services.Author
{
    public class AuthorService(ApplicationDbContext context, IMapper mapper, ILogger<AuthorService> logger) : IAuthorService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthorService> _logger = logger;

        public async Task<GetAuthor> SaveAuthorAsync(PostAuthor postAuthor)
        {
            try
            {
                var author = _mapper.Map<AuthorModel>(postAuthor);
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return _mapper.Map<GetAuthor>(author);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error saving author: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<GetAuthor>> GetAuthorsAsync()
        {
            try
            {
                var authors = await _context.Authors.ToListAsync();
                return _mapper.Map<IEnumerable<GetAuthor>>(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving authors: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<GetAuthor> GetAuthorByIdAsync(long id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                    return null!;

                return _mapper.Map<GetAuthor>(author);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error retrieving author: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<GetAuthor> UpdateAuthorAsync(long id, PutAuthor putAuthor)
        {
            try
            {
                var existAuthor = await _context.Authors.FindAsync(id);
                if (existAuthor == null)
                    return null!;

                _mapper.Map(putAuthor, existAuthor);
                await _context.SaveChangesAsync();
                return _mapper.Map<GetAuthor>(existAuthor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating author: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAuthorAsync(long id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                    return false;

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting author: {Message}", ex.Message);
                throw;
            }
        }
    }
}
