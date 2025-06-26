using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Assignment2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;

        public IEnumerable<NewsArticleDTO> ActiveArticles { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();

        public IndexModel(ILogger<IndexModel> logger, INewsArticleService newsArticleService, ICategoryService categoryService)
        {
            _logger = logger;
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
        }

        public async Task OnGetAsync()
        {
            Categories = await _categoryService.GetActiveCategoriesAsync();
            var allArticles = await _newsArticleService.GetAllNewsArticlesActiveAsync();

            if (SelectedCategoryIds != null && SelectedCategoryIds.Any())
            {
                // Get IDs of active categories
                var activeCategoryIds = Categories.Select(c => c.CategoryId).ToList();

                // Filter by selected categories that are also active
                var validSelectedCategoryIds = SelectedCategoryIds.Where(id => activeCategoryIds.Contains(id)).ToList();

                // Filter articles by valid (active) selected categories
                ActiveArticles = allArticles.Where(a => validSelectedCategoryIds.Contains(a.CategoryId));

                // Update SelectedCategoryIds to only include valid ones
                SelectedCategoryIds = validSelectedCategoryIds;
            }
            else
            {
                // Show articles from active categories only
                var activeCategoryIds = Categories.Select(c => c.CategoryId).ToList();
                ActiveArticles = allArticles.Where(a => activeCategoryIds.Contains(a.CategoryId));
            }
        }
    }
}