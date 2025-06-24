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

namespace Assignment2.Pages.NewsArticle
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add this

        public IndexModel(INewsArticleService newsArticleService, ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor; // Initialize this
        }

        public IEnumerable<NewsArticleDTO> NewsArticles { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }

        [BindProperty]
        public NewsArticleCreateDTO NewsArticle { get; set; }

        [BindProperty]
        public NewsArticleUpdateDTO UpdateNewsArticle { get; set; }

        public async Task OnGetAsync()
        {
            NewsArticles = await _newsArticleService.GetAllNewsArticlesAsync();
            Categories = await _categoryService.GetActiveCategoriesAsync();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload data and return to the same page with validation errors
                NewsArticles = await _newsArticleService.GetAllNewsArticlesAsync();
                Categories = await _categoryService.GetActiveCategoriesAsync();
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
                // Reload data and return to the same page with validation errors
                NewsArticles = await _newsArticleService.GetAllNewsArticlesAsync();
                Categories = await _categoryService.GetActiveCategoriesAsync();
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