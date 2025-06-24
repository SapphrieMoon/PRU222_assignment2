using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly NewsContext _context;
        public NewsArticleRepository(NewsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllNewsArticlesAsync()
        {
            return await _context.NewsArticles
                .Include(na => na.CreatedBy)
                .Include(na => na.UpdatedBy)
                .Include(na => na.Category)
                .ToListAsync();
        }

        public async Task<NewsArticle?> GetNewsArticleByIdAsync(string id)
        {
            return await _context.NewsArticles
                .Include(na => na.CreatedBy)
                .Include(na => na.UpdatedBy)
                .Include(na => na.Category)
                .FirstOrDefaultAsync(a => a.NewsArticleId == id);
        }

        public async Task AddNewsArticleAsync(NewsArticle newsArticle)
        {
            try
            {
                var existingNewsArticle = await GetNewsArticleByIdAsync(newsArticle.NewsArticleId);
                if (existingNewsArticle != null)
                {
                    throw new Exception($"News article with ID {newsArticle.NewsArticleId} already exists.");
                }
                else
                {
                    _context.NewsArticles.Add(newsArticle);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateNewsArticleAsync(NewsArticle newsArticle, int updateUserId) // Changed parameter type from short to int
        {
            try
            {
                var existingNewsArticle = await GetNewsArticleByIdAsync(newsArticle.NewsArticleId);
                if (existingNewsArticle == null)
                {
                    throw new Exception($"News article with ID {newsArticle.NewsArticleId} does not exist.");
                }
                else
                {
                    existingNewsArticle.NewsTitle = newsArticle.NewsTitle;
                    existingNewsArticle.Headline = newsArticle.Headline;
                    existingNewsArticle.NewsContent = newsArticle.NewsContent;
                    existingNewsArticle.NewsSource = newsArticle.NewsSource;
                    existingNewsArticle.NewsStatus = newsArticle.NewsStatus;
                    existingNewsArticle.UpdatedById = updateUserId; // Use the passed updateUserId parameter
                    existingNewsArticle.ModifiedDate = DateTime.UtcNow;

                    // Check if category exists
                    var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == newsArticle.CategoryId);
                    if (category == null)
                    {
                        throw new Exception($"Category with ID {newsArticle.CategoryId} does not exist.");
                    }

                    existingNewsArticle.CategoryId = newsArticle.CategoryId;

                    _context.NewsArticles.Update(existingNewsArticle); // Update the existing article, not the parameter
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteNewsArticleAsync(string id)
        {
            try
            {
                var newsArticle = await GetNewsArticleByIdAsync(id);
                if (newsArticle != null)
                {
                    _context.NewsArticles.Remove(newsArticle);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error deleting news article with ID {id}: {e.Message}");
            }
        }

        public async Task<IEnumerable<NewsArticle>> GetAllNewsArticlesActiveAsync()
        {
            return await _context.NewsArticles
               .Include(na => na.CreatedBy)
               .Include(na => na.UpdatedBy)
               .Include(na => na.Category)
               .Where(c => c.NewsStatus == true)
               .ToListAsync();
        }
    }
}