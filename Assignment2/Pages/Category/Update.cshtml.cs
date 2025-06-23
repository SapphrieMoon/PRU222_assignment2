using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment2.Pages.Category
{
    public class UpdateModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public UpdateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public UpdateCategoryDTO Category { get; set; }

        public SelectList ParentCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var category = await _categoryService.GetCategoryDetailById(id);
            if (category == null)
            {
                return NotFound();
            }

            Category = new UpdateCategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ParentCategoryId = category.ParentCategoryId,
                IsActive = category.IsActive
            };

            var categories = await _categoryService.GetAllCategories();
            ParentCategories = new SelectList(categories, "CategoryId", "CategoryName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllCategories();
                ParentCategories = new SelectList(categories, "CategoryId", "CategoryName");
                return Page();
            }

            try
            {
                await _categoryService.UpdateAsync(Category);
                TempData["SuccessMessage"] = "Category updated successfully.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating category: {ex.Message}");
                var categories = await _categoryService.GetAllCategories();
                ParentCategories = new SelectList(categories, "CategoryId", "CategoryName");
                return Page();
            }
        }
    }
}