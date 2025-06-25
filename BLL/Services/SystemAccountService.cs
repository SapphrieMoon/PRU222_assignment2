using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using static BLL.DTOs.SystemAccountDTO;

namespace BLL.Services
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _accountRepo;

        public SystemAccountService(ISystemAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        // Login (không dùng BCrypt)
        public async Task<AccountResponseDTO?> LoginAsync(LoginDTO dto)
        {
            var account = await _accountRepo.GetByEmailAsync(dto.Email);
            if (account == null || account.AccountPassword != dto.Password)
                return null;

            return new AccountResponseDTO
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole
            };
        }

        // CRUD
        public async Task<IEnumerable<AccountResponseDTO>> GetAllAccountsAsync()
        {
            var accounts = await _accountRepo.GetAllAsync();
            return accounts.Select(a => new AccountResponseDTO
            {
                AccountId = a.AccountId,
                AccountName = a.AccountName,
                AccountEmail = a.AccountEmail,
                AccountRole = a.AccountRole
            });
        }

        public async Task<AccountResponseDTO?> GetAccountByIdAsync(int id)
        {
            var account = await _accountRepo.GetByIdAsync(id);
            return account == null ? null : new AccountResponseDTO
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole
            };
        }

        public async Task CreateAccountAsync(CreateAccountDTO dto)
        {
            var account = new SystemAccount
            {
                AccountName = dto.AccountName,
                AccountEmail = dto.AccountEmail,
                AccountPassword = dto.Password, // Lưu plain text (chỉ dùng cho học tập)
                AccountRole = dto.AccountRole
            };
            await _accountRepo.AddAsync(account);
        }

        public async Task UpdateAccountAsync(UpdateAccountDTO dto)
        {
            // Lấy ID từ DTO thay vì dùng biến id chưa định nghĩa
            var account = await _accountRepo.GetByIdAsync(dto.AccountId);
            if (account == null)
                throw new KeyNotFoundException("Account not found.");

            // Chỉ cập nhật nếu giá trị mới khác null
            account.AccountName = dto.AccountName ?? account.AccountName;
            account.AccountEmail = dto.AccountEmail ?? account.AccountEmail;
            account.AccountPassword = dto.Password ?? account.AccountPassword;
            account.AccountRole = dto.AccountRole ?? account.AccountRole;

            await _accountRepo.UpdateAsync(account);
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _accountRepo.GetByIdAsync(id);
            if (account == null) throw new KeyNotFoundException("Account not found.");

            await _accountRepo.DeleteAsync(id);
        }
    }
}