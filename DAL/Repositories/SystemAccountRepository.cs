using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly NewsContext _context;

        public SystemAccountRepository(NewsContext context)
        {
            _context = context;
        }

        // Login: Lấy tài khoản bằng email (dùng cho đăng nhập)
        public async Task<SystemAccount?> GetByEmailAsync(string email)
        {
            return await _context.SystemAccounts
                .FirstOrDefaultAsync(a => a.AccountEmail == email);
        }

        // CRUD
        public async Task<IEnumerable<SystemAccount>> GetAllAsync()
        {
            return await _context.SystemAccounts.ToListAsync();
        }

        public async Task<SystemAccount?> GetByIdAsync(int id)
        {
            return await _context.SystemAccounts.FindAsync(id);
        }

        public async Task AddAsync(SystemAccount account)
        {
            await _context.SystemAccounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var account = await GetByIdAsync(id);
            if (account != null)
            {
                _context.SystemAccounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        // Kiểm tra tài khoản tồn tại
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.SystemAccounts.AnyAsync(a => a.AccountId == id);
        }

    }
}
