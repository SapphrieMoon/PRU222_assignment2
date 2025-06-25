using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs;
using DAL.Entities;
namespace BLL.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDTO>> GetAllTagsAsync();
        Task<TagDTO?> GetTagByIdAsync(int id);
        Task<IEnumerable<TagDTO>> GetTagsByIdsAsync(List<int> tagIds);
    }


}
