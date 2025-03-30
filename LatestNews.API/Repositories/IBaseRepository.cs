using LatestNews.API.Domain.Common;

namespace LatestNews.API.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
    }
}
