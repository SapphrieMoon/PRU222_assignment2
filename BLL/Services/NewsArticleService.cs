using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using AutoMapper;

namespace BLL.Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepo;
        private readonly IMapper _mapper;

        public NewsArticleService(INewsArticleRepository newsArticleRepo, IMapper mapper)
        {
            _newsArticleRepo = newsArticleRepo;
            _mapper = mapper;
        }

        public async Task<NewsArticleDTO> AddNewsArticleAsync(NewsArticleCreateDTO newsArticle, int currentUserId)
        {
            if (newsArticle == null)
            {
                throw new ArgumentNullException(nameof(newsArticle));
            }

            var article = _mapper.Map<NewsArticle>(newsArticle);

            // Set the current user as both creator and updater
            article.CreatedById = currentUserId;
            article.UpdatedById = currentUserId;
            article.CreatedDate = DateTime.UtcNow;
            article.ModifiedDate = DateTime.UtcNow;

            await _newsArticleRepo.AddNewsArticleAsync(article);

            // Handle tags if provided
            if (newsArticle.TagIds != null && newsArticle.TagIds.Any())
            {
                // Cast to NewsArticleRepository to access the tag method
                if (_newsArticleRepo is DAL.Repositories.NewsArticleRepository repo)
                {
                    await repo.UpdateNewsArticleTagsAsync(article.NewsArticleId, newsArticle.TagIds);
                }
            }

            // Get the created article with all related data
            var createdArticle = await _newsArticleRepo.GetNewsArticleByIdAsync(article.NewsArticleId);
            return _mapper.Map<NewsArticleDTO>(createdArticle);
        }

        public async Task<NewsArticleDTO> DeleteNewsArticleAsync(string id)
        {
            var article = await _newsArticleRepo.GetNewsArticleByIdAsync(id);
            if (article == null)
            {
                throw new KeyNotFoundException($"News article with ID {id} not found.");
            }
            await _newsArticleRepo.DeleteNewsArticleAsync(id);
            return _mapper.Map<NewsArticleDTO>(article);
        }

        public async Task<IEnumerable<NewsArticleDTO>> GetAllNewsArticlesAsync()
        {
            var articles = await _newsArticleRepo.GetAllNewsArticlesAsync();
            return _mapper.Map<IEnumerable<NewsArticleDTO>>(articles);
        }

        public async Task<NewsArticleDTO?> GetNewsArticleByIdAsync(string id)
        {
            var article = await _newsArticleRepo.GetNewsArticleByIdAsync(id);
            if (article == null)
            {
                throw new KeyNotFoundException($"News article with ID {id} not found.");
            }
            return _mapper.Map<NewsArticleDTO>(article);
        }

        public async Task<NewsArticleDTO> UpdateNewsArticleAsync(NewsArticleUpdateDTO newsArticle, int updateUserId)
        {
            if (newsArticle == null)
            {
                throw new ArgumentNullException(nameof(newsArticle));
            }

            // Fetch the existing article from the repository
            var existingArticle = await _newsArticleRepo.GetNewsArticleByIdAsync(newsArticle.NewsArticleId);
            if (existingArticle == null)
            {
                throw new KeyNotFoundException($"News article with ID {newsArticle.NewsArticleId} not found.");
            }

            // Map the updated fields from the DTO to the existing entity
            _mapper.Map(newsArticle, existingArticle);

            // Update the `UpdatedById` and `ModifiedDate` fields
            existingArticle.UpdatedById = updateUserId;
            existingArticle.ModifiedDate = DateTime.UtcNow;

            // Save the basic article changes to the repository
            await _newsArticleRepo.UpdateNewsArticleAsync(existingArticle, updateUserId);

            // Handle tags if provided
            if (newsArticle.TagIds != null)
            {
                // Cast to NewsArticleRepository to access the tag method
                if (_newsArticleRepo is DAL.Repositories.NewsArticleRepository repo)
                {
                    await repo.UpdateNewsArticleTagsAsync(newsArticle.NewsArticleId, newsArticle.TagIds);
                }
            }

            // Get the updated article with all related data
            var updatedArticle = await _newsArticleRepo.GetNewsArticleByIdAsync(newsArticle.NewsArticleId);
            return _mapper.Map<NewsArticleDTO>(updatedArticle);
        }

        public async Task<IEnumerable<NewsArticleDTO>> GetAllNewsArticlesActiveAsync()
        {
            var articles = await _newsArticleRepo.GetAllNewsArticlesActiveAsync();
            return _mapper.Map<IEnumerable<NewsArticleDTO>>(articles);
        }
    }
}