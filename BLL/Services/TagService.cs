using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using static BLL.DTOs.TagDTO;
using DAL.Interfaces;
using DAL.Repositories;
using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepo;
        private readonly IMapper _mapper;
        public TagService(ITagRepository tagRepo, IMapper mapper)
        {
            _tagRepo = tagRepo;
            _mapper = mapper;
        }
        public async Task<TagDTO> GetTagByIdAsync(int id)
        {
            var tag = await _tagRepo.GetByIdAsync(id);
            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with ID {id} not found.");
            }
            return _mapper.Map<TagDTO>(tag);

        }

        public async Task<TagDTO> AddTagAsync(CreateTagDTO createTagDTO)
        {
            if (createTagDTO == null)
            {
                throw new ArgumentNullException(nameof(createTagDTO));
            }
            var tag = _mapper.Map<Tag>(createTagDTO);
            await _tagRepo.AddAsync(tag);

            return _mapper.Map<TagDTO>(tag);
        }
        public async Task<TagDTO> UpdateTagAsync(UpdateTagDTO updateTagDTO)
        {
            if (updateTagDTO == null)
            {
                throw new ArgumentNullException(nameof(updateTagDTO));
            }
            var existingTag = await _tagRepo.GetByIdAsync(updateTagDTO.TagId);
            if (existingTag == null)
            {
                throw new KeyNotFoundException($"Tag with ID {updateTagDTO.TagId} not found.");
            }

            _mapper.Map(updateTagDTO, existingTag);
            await _tagRepo.UpdateAsync(existingTag);

            return _mapper.Map<TagDTO>(existingTag);
        }
        public async Task<TagDTO> DeleteTagAsync(int id)
        {
            var tag = await _tagRepo.GetByIdAsync(id);
            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with ID {id} not found.");
            }

            await _tagRepo.DeleteAsync(id);
            return _mapper.Map<TagDTO>(tag);
        }
        public async Task<IEnumerable<TagDTO>> GetAllTagsAsync()
        {
            var tags = await _tagRepo.GetAllAsyncs();
            return _mapper.Map<IEnumerable<TagDTO>>(tags);
        }

    }
}
