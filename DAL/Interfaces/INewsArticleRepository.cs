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
        Task<NewsArticle?> GetNewsArticleByIdAsync(string id);
        Task AddNewsArticleAsync(NewsArticle newsArticle);
        Task UpdateNewsArticleAsync(NewsArticle newsArticle, int updateUserId); // Changed from short to int
        Task DeleteNewsArticleAsync(string id);
        Task<IEnumerable<NewsArticle>> GetAllNewsArticlesActiveAsync();
    }
}