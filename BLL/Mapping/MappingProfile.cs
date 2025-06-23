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
        }
    }
}