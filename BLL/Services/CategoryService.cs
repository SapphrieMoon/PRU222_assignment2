using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> GetCategoryDetailById(int Id)
        {
            var category = await _repo.GetCategoryDetailById(Id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {Id} not found.");
            }
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = await _repo.GetAllCategories();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<IEnumerable<CategoryDTO>> GetActiveCategoriesAsync()
        {
            var categories = await _repo.GetActiveCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> AddAsync(CreateCategoryDTO createCategoryDTO)
        {
            if (createCategoryDTO == null)
            {
                throw new ArgumentNullException(nameof(createCategoryDTO));
            }

            var category = _mapper.Map<Category>(createCategoryDTO);
            await _repo.AddAsync(category);

            // Return the added category as a DTO
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> UpdateAsync(UpdateCategoryDTO updateCatetoryDTO)
        {
            if (updateCatetoryDTO == null)
            {
                throw new ArgumentNullException(nameof(updateCatetoryDTO));
            }

            var existingCategory = await _repo.GetCategoryDetailById(updateCatetoryDTO.CategoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with Name {updateCatetoryDTO.CategoryName} not found.");
            }

            //cách cũ ko có mapper
            //existingCategory.CategoryName = updateCaretoryDTO.CategoryName ?? existingCategory.CategoryName;
            //existingCategory.CategoryDescription = updateCaretoryDTO.CategoryDescription ?? existingCategory.CategoryDescription;
            //existingCategory.IsActive = updateCaretoryDTO.IsActive ?? existingCategory.IsActive;
            //existingCategory.ParentCategoryId = updateCaretoryDTO.ParentCategoryId ?? existingCategory.ParentCategoryId;

            // Chỉ cập nhật những trường được cung cấp (patch update)
            _mapper.Map(updateCatetoryDTO, existingCategory);

            await _repo.UpdateAsync(existingCategory);
            return _mapper.Map<CategoryDTO>(existingCategory);
        }

        public async Task<CategoryDTO> DeleteAsync(int id)
        {
            var category = await _repo.GetCategoryDetailById(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with Name {category.CategoryName} not found.");
            }

            await _repo.DeleteAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO?> GetParentCategoryAsync(int? id)
        {
            if (!id.HasValue)
            {
                var parentCategory = await _repo.GetParentCategoryAsync();
                return parentCategory != null ? _mapper.Map<CategoryDTO>(parentCategory) : null;
            }

            var category = await _repo.GetParentCategoryAsync();
            return category?.ParentCategory != null ? _mapper.Map<CategoryDTO>(category.ParentCategory) : null;
        }
    }
}
