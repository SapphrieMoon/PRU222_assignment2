
using static BLL.DTOs.SystemAccountDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISystemAccountService
    {
        // Login
        Task<AccountResponseDTO?> LoginAsync(LoginDTO dto);

        // CRUD
        Task<IEnumerable<AccountResponseDTO>> GetAllAccountsAsync();
        Task<AccountResponseDTO?> GetAccountByIdAsync(int id);
        Task CreateAccountAsync(CreateAccountDTO dto);
        Task UpdateAccountAsync(int id, UpdateAccountDTO dto);
        Task DeleteAccountAsync(int id);
    }
}
