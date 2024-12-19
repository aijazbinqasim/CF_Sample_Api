using AutoMapper;
using CF_Sample_Api.AppContext;
using CF_Sample_Api.Contracts;
using CF_Sample_Api.Interfaces;
using CF_Sample_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CF_Sample_Api.Services
{
    public class AuthorService(ApplicationContext context, IMapper mapper, ILogger<AuthorService> logger) : IAuthorService
    {
        private readonly ApplicationContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthorService> _logger = logger;

        public async Task<GetAuthor> SaveAuthorAsync(PostAuthor postAuthor)
        {
            try
            {
                var author = _mapper.Map<AuthorModel>(postAuthor);
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Author saved successfully.");
                return _mapper.Map<GetAuthor>(author);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving author: {ex.Message}");
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
                _logger.LogError($"Error retrieving authors: {ex.Message}");
                throw;
            }
        }

        public async Task<GetAuthor> GetAuthorByIdAsync(long id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if(author == null)
                {
                    _logger.LogWarning($"author with ID {id} not found!");
                    return null!;
                }

                return _mapper.Map<GetAuthor>(author);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving author: {ex.Message}");
                throw;
            }
        }

        public async Task<GetAuthor> UpdateAuthorAsync(long id, PutAuthor putAuthor)
        {
            try
            {
                var existAuthor = await _context.Authors.FindAsync(id);
                if (existAuthor == null)
                {
                    _logger.LogWarning($"Author with ID {id} not found!");
                    return null!;
                }

                _mapper.Map(putAuthor, existAuthor);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Author updated successfully.");
                return _mapper.Map<GetAuthor>(existAuthor);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating author: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAuthorAsync(long id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if(author == null)
                {
                    _logger.LogWarning($"Author with ID {id} not found!");
                    return false;
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Author with ID {id} deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting author: {ex.Message}");
                throw;
            }
        }
    }
}
