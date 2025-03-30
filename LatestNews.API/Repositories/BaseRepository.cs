using LatestNews.API.DataAccessManager.EFCore.Contexts;
using LatestNews.API.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LatestNews.API.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
