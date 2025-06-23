using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<DTOs.CategoryDTO> GetCategoryDetailById(int id); // Lấy chi tiết danh mục theo ID
        Task<IEnumerable<CategoryDTO>> GetAllCategories(); // Lấy tất cả danh mục
        Task<IEnumerable<CategoryDTO>> GetActiveCategoriesAsync(); // Lấy danh mục đang hoạt động
        Task<CategoryDTO> AddAsync(CreateCategoryDTO category); // Thêm một danh mục mới
        Task<CategoryDTO> DeleteAsync(int id);
        Task<CategoryDTO> UpdateAsync(UpdateCategoryDTO category); // Cập nhật một danh mục
        Task<CategoryDTO?> GetParentCategoryAsync(int? parentId); // Lấy danh mục cha của một danh mục cụ thể (có thể cần thêm tham số ID hoặc CategoryId)
    }
}
