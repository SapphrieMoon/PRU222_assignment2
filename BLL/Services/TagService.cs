using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagDTO>> GetAllTagsAsync()
        {
            var tags = await _tagRepository.GetAllTagsAsync();
            return _mapper.Map<IEnumerable<TagDTO>>(tags);
        }

        public async Task<TagDTO?> GetTagByIdAsync(int id)
        {
            var tag = await _tagRepository.GetTagByIdAsync(id);
            return _mapper.Map<TagDTO>(tag);
        }

        public async Task<IEnumerable<TagDTO>> GetTagsByIdsAsync(List<int> tagIds)
        {
            var tags = await _tagRepository.GetTagsByIdsAsync(tagIds);
            return _mapper.Map<IEnumerable<TagDTO>>(tags);
        }

    }
}
