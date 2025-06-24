using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface INewsArticleService
    {
        Task<IEnumerable<NewsArticleDTO>> GetAllNewsArticlesAsync();
        Task<NewsArticleDTO?> GetNewsArticleByIdAsync(string id);
        Task<NewsArticleDTO> AddNewsArticleAsync(NewsArticleCreateDTO newsArticle, int currentUserId); // Added currentUserId parameter
        Task<NewsArticleDTO> UpdateNewsArticleAsync(NewsArticleUpdateDTO newsArticle, int updateUserId); // Changed from short to int
        Task<NewsArticleDTO> DeleteNewsArticleAsync(string id);
        Task<IEnumerable<NewsArticleDTO>> GetAllNewsArticlesActiveAsync();
    }
}