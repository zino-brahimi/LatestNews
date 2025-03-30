using LatestNews.API.DataAccessManager.EFCore.Common;
using LatestNews.API.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LatestNews.API.DataAccessManager.EFCore.Configurations
{
    public class ArticleConfiguration : BaseEntityConfiguration<Article>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            base.Configure(builder);

            builder.HasIndex(e => e.Source);

            builder.HasIndex(a => new { a.Title, a.Source })
            .IsUnique();
        }
    }
}
