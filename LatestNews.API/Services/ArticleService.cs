using LatestNews.API.Domain.Entities;
using LatestNews.API.Dtos;
using LatestNews.API.Helpers;
using LatestNews.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LatestNews.API.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<ArticleDto>> GetAllAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = _repository.GetAll();

            var articlesQuery = query.Select(o => new ArticleDto
            {
                Description = o.Description,
                PublishedAt = o.PublishedAt,
                Source = o.Source,
                Title = o.Title,
                Url = o.Url,
                UrlToImage = o.UrlToImage
            });

            var articles = await PagedResult<ArticleDto>.CreateAsync(articlesQuery, page, pageSize, cancellationToken);
            return articles;
        }

        public async Task AddAsync(ArticleDto articleDto, CancellationToken cancellationToken = default)
        {
            var article = new Article
            {
                Description = articleDto.Description,
                PublishedAt = articleDto.PublishedAt,
                Source = articleDto.Source,
                Title = articleDto.Title,
                Url = articleDto.Url,
                UrlToImage = articleDto.UrlToImage
            };

            await _repository.AddAsync(article, cancellationToken);
        }

        public async Task<PagedResult<ArticleDto>> GetBySourceAsync(string source, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = _repository.GetBySource(source);

            var articlesQuery = query.Select(o => new ArticleDto
            {
                Description = o.Description,
                PublishedAt = o.PublishedAt,
                Source = o.Source,
                Title = o.Title,
                Url = o.Url,
                UrlToImage = o.UrlToImage
            });

            var articles = await PagedResult<ArticleDto>.CreateAsync(articlesQuery, page, pageSize, cancellationToken);
            return articles;
        }
    }
}
