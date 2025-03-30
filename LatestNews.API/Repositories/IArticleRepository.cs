using LatestNews.API.Domain.Entities;

namespace LatestNews.API.Repositories
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        IQueryable<Article> GetBySource(string source);
    }
}
