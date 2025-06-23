using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface INewsArticleRepository
    {
        // CRUD operations for NewsArticle
        Task<IEnumerable<NewsArticle>> GetAllNewsArticlesAsync();
        Task<NewsArticle?> GetNewsArticleByIdAsync(int id);
        Task AddNewsArticleAsync(NewsArticle newsArticle);
        Task UpdateNewsArticleAsync(NewsArticle newsArticle);
        Task DeleteNewsArticleAsync(int id);

        // Optional: Check if a news article exists
        Task<bool> NewsArticleExistsAsync(int id);
        // Optional: Get news articles by category ID
        Task<IEnumerable<NewsArticle>> GetNewsArticlesByCategoryIdAsync(int categoryId);
        // Optional: Get news articles by title
        Task<IEnumerable<NewsArticle>> GetNewsArticlesByTitleAsync(string title);
        // Optional: Get news articles by tag ID
        Task<IEnumerable<NewsArticle>> GetNewsArticlesByTagIdAsync(int tagId);


    }
}
