using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsyncs();
        Task<Tag?> GetByIdAsync(int id);
        Task AddAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(int id);
    }
}
