using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category mappings
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.ParentCategoryId,
                          opt => opt.MapFrom(src => src.ParentCategoryId ?? 0)); // Xử lý null

            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();

            // NewsArticle mappings - ADD THESE MISSING MAPPINGS
            CreateMap<NewsArticle, NewsArticleDTO>();
            CreateMap<NewsArticleDTO, NewsArticle>();
            CreateMap<NewsArticleCreateDTO, NewsArticle>()
                .ForMember(dest => dest.NewsArticleId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<NewsArticleUpdateDTO, NewsArticle>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<NewsArticle, NewsArticleDTO>()
               .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.AccountName : null))
               .ForMember(dest => dest.UpdatedByName, opt => opt.MapFrom(src => src.UpdatedBy != null ? src.UpdatedBy.AccountName : null))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null));

            // Map from NewsArticleCreateDTO to NewsArticle entity
            CreateMap<NewsArticleCreateDTO, NewsArticle>();

            // Map from NewsArticleUpdateDTO to NewsArticle entity
            CreateMap<NewsArticleUpdateDTO, NewsArticle>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}