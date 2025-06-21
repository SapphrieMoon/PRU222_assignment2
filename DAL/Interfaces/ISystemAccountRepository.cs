using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISystemAccountRepository
    {
        // Login
        Task<SystemAccount?> GetByEmailAsync(string email);

        // CRUD
        Task<IEnumerable<SystemAccount>> GetAllAsync();
        Task<SystemAccount?> GetByIdAsync(int id);
        Task AddAsync(SystemAccount account);
        Task UpdateAsync(SystemAccount account);
        Task DeleteAsync(int id);

        // Optional: Kiểm tra tồn tại
        Task<bool> ExistsAsync(int id);
    }
}
