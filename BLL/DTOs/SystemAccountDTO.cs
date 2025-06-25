using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class SystemAccountDTO
    {
        // Login
        public class LoginDTO
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }

        // Trả về thông tin tài khoản
        public class AccountResponseDTO
        {
            public int AccountId { get; set; }
            public required string AccountName { get; set; }
            public required string AccountEmail { get; set; }
            public int AccountRole { get; set; }
        }

        // Tạo tài khoản
        public class CreateAccountDTO
        {
            public required string AccountName { get; set; }
            public required string AccountEmail { get; set; }
            public required string Password { get; set; }
            public int AccountRole { get; set; }
        }

        // Cập nhật tài khoản
        public class UpdateAccountDTO
        {
            public int AccountId { get; set; }
            public string? AccountName { get; set; }
            public string? AccountEmail { get; set; }
            public string? Password { get; set; }
            public int? AccountRole { get; set; }
        }
    }
}
