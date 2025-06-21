using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static BLL.DTOs.SystemAccountDTO;

namespace Assignment2.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ISystemAccountService _accountService;

        [BindProperty]
        public LoginDTO LoginDTO { get; set; }

        public string ErrorMessage { get; set; }

        public LoginModel(ISystemAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet()
        {
            // Hiển thị form login
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var account = await _accountService.LoginAsync(LoginDTO);
            if (account == null)
            {
                ErrorMessage = "Email hoặc mật khẩu không đúng.";
                return Page();
            }

            // Lưu thông tin đăng nhập (Session/Cookie)
            HttpContext.Session.SetInt32("AccountId", account.AccountId);
            return RedirectToPage("/Index");
        }
    }
}
