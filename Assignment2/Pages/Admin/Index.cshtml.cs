using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using BLL.DTOs;
using static BLL.DTOs.SystemAccountDTO;
using System.Text.Json;

namespace Assignment2.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountService _accountService;

        public IndexModel(ISystemAccountService accountService)
        {
            _accountService = accountService;
        }

        public IEnumerable<AccountResponseDTO> Accounts { get; set; } = new List<AccountResponseDTO>();

        public async Task OnGetAsync()
        {
            try
            {
                Accounts = await _accountService.GetAllAccountsAsync();
            }
            catch (Exception ex)
            {
                // Log error (you can use ILogger here)
                ModelState.AddModelError("", "Error loading accounts: " + ex.Message);
                Accounts = new List<AccountResponseDTO>();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                var createDto = JsonSerializer.Deserialize<CreateAccountDTO>(requestBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (createDto == null)
                {
                    return BadRequest("Invalid data");
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(createDto.AccountName) ||
                    string.IsNullOrWhiteSpace(createDto.AccountEmail) ||
                    string.IsNullOrWhiteSpace(createDto.Password))
                {
                    return BadRequest("All fields are required");
                }

                // Validate email format
                if (!IsValidEmail(createDto.AccountEmail))
                {
                    return BadRequest("Invalid email format");
                }

                // Check if email already exists
                var existingAccounts = await _accountService.GetAllAccountsAsync();
                if (existingAccounts.Any(a => a.AccountEmail.Equals(createDto.AccountEmail, StringComparison.OrdinalIgnoreCase)))
                {
                    return BadRequest("Email already exists");
                }

                await _accountService.CreateAccountAsync(createDto);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return BadRequest("Error creating account: " + ex.Message);
            }
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            try
            {
                var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                var updateDto = JsonSerializer.Deserialize<UpdateAccountDTO>(requestBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (updateDto == null || updateDto.AccountId <= 0)
                {
                    return BadRequest("Invalid data");
                }

                // Validate email format if provided
                if (!string.IsNullOrWhiteSpace(updateDto.AccountEmail) && !IsValidEmail(updateDto.AccountEmail))
                {
                    return BadRequest("Invalid email format");
                }

                // Check if email already exists (excluding current account)
                if (!string.IsNullOrWhiteSpace(updateDto.AccountEmail))
                {
                    var existingAccounts = await _accountService.GetAllAccountsAsync();
                    if (existingAccounts.Any(a => a.AccountEmail.Equals(updateDto.AccountEmail, StringComparison.OrdinalIgnoreCase)
                                                && a.AccountId != updateDto.AccountId))
                    {
                        return BadRequest("Email already exists");
                    }
                }

                await _accountService.UpdateAccountAsync(updateDto);
                return new OkResult();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Account not found");
            }
            catch (Exception ex)
            {
                return BadRequest("Error updating account: " + ex.Message);
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid account ID");
                }

                await _accountService.DeleteAccountAsync(id);
                return new OkResult();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Account not found");
            }
            catch (Exception ex)
            {
                return BadRequest("Error deleting account: " + ex.Message);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}