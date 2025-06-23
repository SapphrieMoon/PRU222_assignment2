using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITag
    {
        Task<IEnumerable<Tag>> GetAllAsyncs();
        Task<Tag?> GetByIdAsync(int id);
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(int id);
        // Optional: Check if a tag exists
        Task<bool> ExistsAsync(int id);
        // Optional: Get tags by news article ID
        Task<IEnumerable<Tag>> GetTagsByNewsArticleIdAsync(int newsArticleId);
        // Optional: Get tags by name
        Task<IEnumerable<Tag>> GetTagsByNameAsync(string name);

    }
}
