using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using BLL.DTOs;

namespace Assignment2.Pages.NewsArticle
{
    public class DetailModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public DetailModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public NewsArticleDTO NewsArticle { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "News article not found.";
                return RedirectToPage("Index");
            }

            try
            {
                NewsArticle = await _newsArticleService.GetNewsArticleByIdAsync(id);

                if (NewsArticle == null)
                {
                    TempData["ErrorMessage"] = "News article not found.";
                    return RedirectToPage("Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the news article.";
                return RedirectToPage("Index");
            }
        }
    }
}