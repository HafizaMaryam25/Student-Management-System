using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentManagementSystem.Areas.Identity.Pages.Account
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["SnackbarMessage"] = "Logout successful!";
            TempData["SnackbarType"] = "success";

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToPage("/Account/Login");
        }
    }
}
