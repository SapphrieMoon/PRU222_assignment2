using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace DAL.Repositories
{
    public class NewsArticleRepository
    {
        private readonly NewsContext _context;
        public NewsArticleRepository(NewsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<NewsArticle>> GetAllNewsArticlesAsync()
        {
            return await _context.NewsArticles.ToListAsync();
        }
        public async Task<NewsArticle?> GetNewsArticleByIdAsync(int id)
        {
            return await _context.NewsArticles.FindAsync(id);
        }
        public async Task AddNewsArticleAsync(NewsArticle newsArticle)
        {
            await _context.NewsArticles.AddAsync(newsArticle);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateNewsArticleAsync(NewsArticle newsArticle)
        {
            _context.NewsArticles.Update(newsArticle);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteNewsArticleAsync(int id)
        {
            var newsArticle = await GetNewsArticleByIdAsync(id);
            if (newsArticle != null)
            {
                _context.NewsArticles.Remove(newsArticle);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<NewsArticle>> GetNewsArticlesByCategoryIdAsync(int categoryId)
        {
            return await _context.NewsArticles
                .Where(a => a.CategoryId == categoryId)
                .ToListAsync();
        }
        public async Task<IEnumerable<NewsArticle>> GetNewsArticlesByTitleAsync(string title)
        {
            return await _context.NewsArticles
                .Where(a => a.NewsTitle.Contains(title))
                .ToListAsync();
        }
        public async Task<IEnumerable<NewsArticle>> GetNewsArticlesByTagIdAsync(int tagId)
        {
            return await _context.NewsArticles
                .Where(a => a.NewsTags.Any(t => t.TagId == tagId))
                .ToListAsync();
        }
    }
}
