#nullable disable
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly FirebaseAuthService _firebaseAuth;
        private readonly FirestoreService _firestore;

        public RegisterModel(FirebaseAuthService firebaseAuth, FirestoreService firestore)
        {
            _firebaseAuth = firebaseAuth;
            _firestore = firestore;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string SnackbarMessage { get; set; }

        [TempData]
        public string SnackbarType { get; set; }

        public class InputModel
        {
            [Required, EmailAddress]
            public string Email { get; set; }

            [Required, StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Password match nahi ho raha")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["SnackbarMessage"] = "Please fill all fields correctly!";
                ViewData["SnackbarType"] = "error";
                return Page();
            }

            try
            {
                var result = await _firebaseAuth.RegisterAsync(Input.Email, Input.Password);
                await _firestore.SetUserAsync(result.UserId, result.Email, "Student");

                TempData["SnackbarMessage"] = "Registration successful! Please login.";
                TempData["SnackbarType"] = "success";

                return RedirectToPage("/Account/Login");
            }
            catch (FirebaseAuthServiceException ex)
            {
                var message = ex.Message.Contains("EMAIL_EXISTS", StringComparison.OrdinalIgnoreCase)
                    ? "User already registered!"
                    : "Registration failed! Please check your email and password.";

                ViewData["SnackbarMessage"] = message;
                ViewData["SnackbarType"] = "error";
                return Page();
            }
        }
    }
}
