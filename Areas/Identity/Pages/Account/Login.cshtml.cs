#nullable disable
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly FirebaseAuthService _firebaseAuth;
        private readonly FirestoreService _firestore;

        public LoginModel(FirebaseAuthService firebaseAuth, FirestoreService firestore)
        {
            _firebaseAuth = firebaseAuth;
            _firestore = firestore;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string SnackbarMessage { get; set; }

        [TempData]
        public string SnackbarType { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");

            if (!ModelState.IsValid)
            {
                ViewData["SnackbarMessage"] = "Please enter valid login details!";
                ViewData["SnackbarType"] = "error";
                return Page();
            }

            try
            {
                var result = await _firebaseAuth.LoginAsync(Input.Email, Input.Password);
                var role = await _firestore.GetUserRoleAsync(result.UserId);

                // Every account created through this application is a Student unless
                // its Firebase/Firestore role has explicitly been set to Admin.
                if (string.IsNullOrWhiteSpace(role))
                {
                    role = "Student";
                    await _firestore.SetUserAsync(result.UserId, result.Email, role);
                }

                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, result.UserId),
                    new(ClaimTypes.Name, result.Email),
                    new(ClaimTypes.Email, result.Email),
                    new(ClaimTypes.Role, role)
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = Input.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    properties);

                TempData["SnackbarMessage"] = "Login successful!";
                TempData["SnackbarType"] = "success";

                if (role == "Admin")
                    return RedirectToAction("Index", "StudentDetails");

                var student = await _firestore.GetStudentByUserIdAsync(result.UserId);
                if (student != null)
                    return RedirectToAction("Details", "StudentDetails", new { id = student.Id });

                return RedirectToAction("Create", "StudentDetails");
            }
            catch (FirebaseAuthServiceException)
            {
                ViewData["SnackbarMessage"] = "Invalid email or password!";
                ViewData["SnackbarType"] = "error";
                return Page();
            }
        }
    }
}
