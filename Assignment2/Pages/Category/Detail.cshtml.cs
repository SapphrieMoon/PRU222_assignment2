using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2.Pages.Category
{
    public class DetailModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public DetailModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public CategoryDTO Category { get; set; }
        public string ParentCategoryName { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _categoryService.GetCategoryDetailById(id);
            if (Category == null)
            {
                return NotFound();
            }

            if (Category.ParentCategoryId > 0)
            {
                var parent = await _categoryService.GetCategoryDetailById(Category.ParentCategoryId);
                ParentCategoryName = parent?.CategoryName ?? "N/A";
            }

            return Page();
        }
    }
}