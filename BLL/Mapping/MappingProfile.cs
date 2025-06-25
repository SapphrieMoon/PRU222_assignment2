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
                          opt => opt.MapFrom(src => src.ParentCategoryId ?? 0));

            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();

            // Tag mappings - ADD THESE
            CreateMap<Tag, TagDTO>();
            CreateMap<TagDTO, Tag>();

            // NewsArticle mappings with tag support
            CreateMap<NewsArticle, NewsArticleDTO>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy != null ? src.CreatedBy.AccountName : null))
                .ForMember(dest => dest.UpdatedByName, opt => opt.MapFrom(src => src.UpdatedBy != null ? src.UpdatedBy.AccountName : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null))
                .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.NewsTags.Select(nt => nt.TagId).ToList()))
                .ForMember(dest => dest.TagNames, opt => opt.MapFrom(src => src.NewsTags.Select(nt => nt.Tag.TagName).ToList()));

            CreateMap<NewsArticleDTO, NewsArticle>();

            CreateMap<NewsArticleCreateDTO, NewsArticle>()
                .ForMember(dest => dest.NewsArticleId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.NewsTags, opt => opt.Ignore()); // Handle tags separately

            CreateMap<NewsArticleUpdateDTO, NewsArticle>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.NewsTags, opt => opt.Ignore()) // Handle tags separately
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}