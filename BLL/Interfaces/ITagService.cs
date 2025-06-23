using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
namespace BLL.Interfaces
{
    public interface ITagService
    {
        // CRUD operations for Tag
        Task<IEnumerable<TagDTO>> GetAllTagsAsync();
        Task<TagDTO?> GetTagByIdAsync(int id);
        Task<TagDTO> AddTagAsync(CreateTagDTO tag);
        Task<TagDTO> UpdateTagAsync(UpdateTagDTO tag);
        Task<TagDTO> DeleteTagAsync(int id);
    }


}
