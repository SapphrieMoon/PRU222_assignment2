using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            //// Xóa toàn bộ session liên quan đến đăng nhập
            //HttpContext.Session.Remove("AccountId");
            //HttpContext.Session.Remove("AccountRole");
            ////HttpContext.Session.Remove("AccountName");

            // Hoặc xóa toàn bộ session (nếu muốn)
            HttpContext.Session.Clear();

            return RedirectToPage("/Account/Login");
        }

        // Xử lý cả trường hợp GET request (nếu cần)
        public IActionResult OnGet()
        {
            return OnPost();
        }
    }
}
