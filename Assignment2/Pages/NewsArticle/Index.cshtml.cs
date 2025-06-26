using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Entities;
using BLL.Interfaces;
using BLL.DTOs;
using Microsoft.AspNetCore.Http; // Add this for session support
using Microsoft.AspNetCore.SignalR;
using Assignment2.Hubs;

namespace Assignment2.Pages.NewsArticle
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService; // Add tag service
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<NewsHub> _newsHubContext;

        public IndexModel(INewsArticleService newsArticleService, ICategoryService categoryService,
            ITagService tagService, IHttpContextAccessor httpContextAccessor, IHubContext<NewsHub> newsHubContext)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _tagService = tagService; // Initialize tag service
            _httpContextAccessor = httpContextAccessor;
            _newsHubContext = newsHubContext;
        }

        public IEnumerable<NewsArticleDTO> NewsArticles { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; } // Add tags property

        [BindProperty]
        public NewsArticleCreateDTO NewsArticle { get; set; }

        [BindProperty]
        public NewsArticleUpdateDTO UpdateNewsArticle { get; set; }

        public async Task OnGetAsync()
        {
            NewsArticles = await _newsArticleService.GetAllNewsArticlesAsync();
            Categories = await _categoryService.GetActiveCategoriesAsync();
            Tags = await _tagService.GetAllTagsAsync(); // Load all tags
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                NewsArticles = await _newsArticleService.GetAllNewsArticlesAsync();
                Categories = await _categoryService.GetActiveCategoriesAsync();
                Tags = await _tagService.GetAllTagsAsync(); // Reload tags
                TempData["ErrorMessage"] = "Please correct the errors and try again.";
                return Page();
            }

            try
            {
                // Get current user's AccountId from session
                var currentAccountId = _httpContextAccessor.HttpContext.Session.GetInt32("AccountId");

                if (!currentAccountId.HasValue)
                {
                    TempData["ErrorMessage"] = "User session expired. Please log in again.";
                    return RedirectToPage("/Account/Login");
                }

                await _newsArticleService.AddNewsArticleAsync(NewsArticle, currentAccountId.Value);
                TempData["SuccessMessage"] = "News article created successfully.";
                // Broadcast to SignalR
                await _newsHubContext.Clients.All.SendAsync("NewsArticleCreated");
            }
            catch (ArgumentNullException ex)
            {
                TempData["ErrorMessage"] = "Invalid data provided.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the news article.";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                NewsArticles = await _newsArticleService.GetAllNewsArticlesAsync();
                Categories = await _categoryService.GetActiveCategoriesAsync();
                Tags = await _tagService.GetAllTagsAsync(); // Reload tags
                TempData["ErrorMessage"] = "Please correct the errors and try again.";
                return Page();
            }

            try
            {
                // Get current user's AccountId from session
                var currentAccountId = _httpContextAccessor.HttpContext.Session.GetInt32("AccountId");

                if (!currentAccountId.HasValue)
                {
                    TempData["ErrorMessage"] = "User session expired. Please log in again.";
                    return RedirectToPage("/Account/Login");
                }

                await _newsArticleService.UpdateNewsArticleAsync(UpdateNewsArticle, currentAccountId.Value);
                TempData["SuccessMessage"] = "News article updated successfully.";
                // Broadcast to SignalR
                await _newsHubContext.Clients.All.SendAsync("NewsArticleUpdated");
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (ArgumentNullException ex)
            {
                TempData["ErrorMessage"] = "Invalid data provided.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the news article.";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            try
            {
                await _newsArticleService.DeleteNewsArticleAsync(id);
                TempData["SuccessMessage"] = "News article deleted successfully.";
                // Broadcast to SignalR
                await _newsHubContext.Clients.All.SendAsync("NewsArticleDeleted");
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the news article.";
            }

            return RedirectToPage();
        }
    }
}