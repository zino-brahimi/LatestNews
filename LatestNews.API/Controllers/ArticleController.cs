using LatestNews.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LatestNews.API.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var articles = await _articleService.GetAllAsync(page, pageSize, cancellationToken);
            return Ok(articles);
        }

        [HttpGet("by-source")]
        public async Task<IActionResult> GetAllAsync([FromQuery] string source, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var articles = await _articleService.GetBySourceAsync(source, page, pageSize, cancellationToken);
            return Ok(articles);
        }
    }
}
