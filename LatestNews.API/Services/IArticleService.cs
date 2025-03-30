using LatestNews.API.Domain.Entities;
using LatestNews.API.Dtos;
using LatestNews.API.Helpers;

namespace LatestNews.API.Services
{
    public interface IArticleService
    {
        Task<PagedResult<ArticleDto>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task AddAsync(ArticleDto article, CancellationToken cancellationToken = default);
        Task<PagedResult<ArticleDto>> GetBySourceAsync(string source, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    }
}
