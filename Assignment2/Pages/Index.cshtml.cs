using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INewsArticleService _newsArticleService;

        public IEnumerable<NewsArticleDTO> ActiveArticles { get; set; }

        public IndexModel(ILogger<IndexModel> logger, INewsArticleService newsArticleService)
        {
            _logger = logger;
            _newsArticleService = newsArticleService;
        }

        public async Task OnGetAsync()
        {
            ActiveArticles = await _newsArticleService.GetAllNewsArticlesActiveAsync();
        }
    }
}
