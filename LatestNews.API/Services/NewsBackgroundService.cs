

using LatestNews.API.DataAccessManager.EFCore.Contexts;
using LatestNews.API.Domain.Entities;
using LatestNews.API.Dtos;
using LatestNews.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LatestNews.API.Services
{
    public class NewsBackgroundService(
        HttpClient httpClient,
        IServiceScopeFactory serviceScopeFactory,
        IConfiguration configuration,
        ILogger<NewsBackgroundService> logger) : BackgroundService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<NewsBackgroundService> _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                        string apiKey = _configuration["ApiKey"] ?? "";

                        if (string.IsNullOrEmpty(apiKey)) return;

                        string url = $"https://newsapi.org/v2/top-headlines?category=general&apiKey={apiKey}";

                        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# App");

                        var response = await _httpClient.GetAsync(url, stoppingToken);
                        if (!response.IsSuccessStatusCode)
                        {
                            _logger.LogError($"Error fetching news: {response.ReasonPhrase}");
                            return;
                        }

                        var jsonString = await response.Content.ReadAsStringAsync(stoppingToken);
                        if (!response.IsSuccessStatusCode) return;

                        var newsResponse = JsonConvert.DeserializeObject<NewsApiResponse>(jsonString);
                        if (newsResponse == null || newsResponse.TotalResults == 0 || newsResponse.Status != "ok")
                            return;

                        var existingArticles = await dbContext.Articles.Select(o => new
                        {
                            o.Title,
                            o.PublishedAt
                        }).ToListAsync(stoppingToken);

                        var newArticles = newsResponse.Articles.Where(article => !existingArticles.Any(e => e.Title == article.Title && e.PublishedAt == article.PublishedAt)).ToList();

                        if (newArticles.Count != 0)
                        {
                            var newsEntities = newArticles.Select(article => new Article
                            {
                                Title = article.Title,
                                Description = article.Description,
                                Url = article.Url,
                                UrlToImage = article.UrlToImage,
                                Source = article.Source?.Name,
                                PublishedAt = article.PublishedAt
                            }).ToList();

                            await dbContext.Articles.AddRangeAsync(newsEntities, stoppingToken);
                            await dbContext.SaveChangesAsync(stoppingToken);
                        }

                        await dbContext.SaveChangesAsync(stoppingToken);
                    }

                    await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception in NewsBackgroundService: {ex.Message}");
                }
            }
        }

        public class NewsApiResponse
        {
            public string? Status { get; set; }
            public int TotalResults { get; set; }
            public List<ArticlesResponse> Articles { get; set; } = [];
        }

        public class ArticlesResponse
        {
            public SourceResponse? Source { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Url { get; set; }
            public string? UrlToImage { get; set; }
            public string? Content { get; set; }
            public DateTime PublishedAt { get; set; }
        }

        public class SourceResponse
        {
            public string? Id { get; set; }
            public string? Name { get; set; }
        }
    }
}
