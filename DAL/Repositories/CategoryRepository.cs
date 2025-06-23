using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NewsContext _context;

        public CategoryRepository(NewsContext context) 
        {
            _context = context;
        }

        public async Task<Category> GetCategoryDetailById(int id)
        {
            //return await _context.Categories.FindAsync(id);
            //return await _context.Categories.FirstAsync(c => c.CategoryId == id);
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.IsActive == true)
                .Include(c => c.ParentCategory)
                .ToListAsync();
        }

        public async Task AddAsync(Category category) 
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Category?> GetParentCategoryAsync()
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.ParentCategoryId == null);
        }
    }
}
