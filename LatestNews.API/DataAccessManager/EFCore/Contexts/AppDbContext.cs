using LatestNews.API.DataAccessManager.EFCore.Configurations;
using LatestNews.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LatestNews.API.DataAccessManager.EFCore.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        }
    }
}
