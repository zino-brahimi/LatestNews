using LatestNews.API.Domain.Common;

namespace LatestNews.API.Domain.Entities
{
    public class Article : BaseEntity  
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? UrlToImage { get; set; }
        public string? Source { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
