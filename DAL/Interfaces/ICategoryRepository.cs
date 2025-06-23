using DAL.Entities;


namespace DAL.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryDetailById(int id); // Hàm này dùng để lấy chi tiết của một danh mục theo ID
        Task<IEnumerable<Category>> GetAllCategories(); // Hàm này dùng để lấy tất cả danh mục
        Task<IEnumerable<Category>> GetActiveCategoriesAsync(); // Hàm này dùng để lấy danh mục đang hoạt động
        Task AddAsync(Category category); // Hàm này dùng để thêm một danh mục mới
        Task UpdateAsync(Category category); // Hàm này dùng để cập nhật một danh mục
        Task DeleteAsync(int id); // Hàm này dùng để xóa một danh mục theo ID
        Task<Category?> GetParentCategoryAsync(); // Hàm này dùng để lấy danh mục cha của một danh mục cụ thể (có thể cần thêm tham số ID hoặc CategoryId)
    }
}
