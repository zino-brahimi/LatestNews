using LatestNews.API.DataAccessManager.EFCore.Contexts;
using LatestNews.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LatestNews.API.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Article> GetBySource(string source)
        {
            var articles = _dbSet.AsNoTracking().Where(o => o.Source == source);
            return articles;
        }        
    }
}
