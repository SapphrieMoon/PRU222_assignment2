using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment2.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CreateCategoryDTO Category { get; set; }

        public SelectList ParentCategories { get; set; }

        public async Task OnGetAsync()
        {
            var categories = await _categoryService.GetAllCategories();
            ParentCategories = new SelectList(categories, "CategoryId", "CategoryName");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            try
            {
                await _categoryService.AddAsync(Category);
                TempData["SuccessMessage"] = "Category created successfully.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating category: {ex.Message}");
                await OnGetAsync();
                return Page();
            }
        }
    }
}