using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Controllers
{
    [Authorize]
    public class StudentDetailsController : Controller
    {
        private readonly FirestoreService _firestore;

        public StudentDetailsController(FirestoreService firestore)
        {
            _firestore = firestore;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var students = await _firestore.GetStudentsAsync();
            return View(students);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyProfile()
        {
            var userId = GetCurrentUserId();
            var student = await _firestore.GetStudentByUserIdAsync(userId);

            if (student == null)
            {
                TempData["SnackbarMessage"] = "You have not created your profile yet!";
                TempData["SnackbarType"] = "warning";
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Details), new { id = student.Id });
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["SnackbarMessage"] = "Student not found!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(Index));
            }

            var student = await _firestore.GetStudentByIdAsync(id);

            if (student == null)
            {
                TempData["SnackbarMessage"] = "Student record does not exist!";
                TempData["SnackbarType"] = "error";
                return User.IsInRole("Admin")
                    ? RedirectToAction(nameof(Index))
                    : RedirectToAction(nameof(MyProfile));
            }

            if (User.IsInRole("Student") && student.Id != GetCurrentUserId())
            {
                TempData["SnackbarMessage"] = "You are not allowed to view other students!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(MyProfile));
            }

            return View(student);
        }

        [Authorize(Roles = "Student")]
        public IActionResult Create()
        {
            return View(new StudentDetail { Email = User.Identity?.Name ?? string.Empty });
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDetail studentDetail)
        {
            if (!ModelState.IsValid)
            {
                ViewData["SnackbarMessage"] = "Please enter valid student details!";
                ViewData["SnackbarType"] = "error";
                return View(studentDetail);
            }

            var userId = GetCurrentUserId();
            var email = User.Identity?.Name ?? string.Empty;
            var existing = await _firestore.GetStudentByUserIdAsync(userId);

            if (existing != null)
            {
                TempData["SnackbarMessage"] = "Your profile already exists!";
                TempData["SnackbarType"] = "warning";
                return RedirectToAction(nameof(MyProfile));
            }

            studentDetail.Id = userId;
            studentDetail.UserId = userId;
            studentDetail.Email = email;

            await _firestore.CreateStudentAsync(userId, studentDetail);

            TempData["SnackbarMessage"] = "Your profile has been created successfully!";
            TempData["SnackbarType"] = "success";

            return RedirectToAction(nameof(MyProfile));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["SnackbarMessage"] = "Student not found!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(Index));
            }

            if (User.IsInRole("Student") && id != GetCurrentUserId())
            {
                TempData["SnackbarMessage"] = "You are not allowed to edit other students!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(MyProfile));
            }

            var student = await _firestore.GetStudentByIdAsync(id);
            if (student == null)
            {
                TempData["SnackbarMessage"] = "Student record does not exist!";
                TempData["SnackbarType"] = "error";
                return User.IsInRole("Admin")
                    ? RedirectToAction(nameof(Index))
                    : RedirectToAction(nameof(MyProfile));
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, StudentDetail studentDetail)
        {
            if (string.IsNullOrWhiteSpace(id) || id != studentDetail.Id)
            {
                TempData["SnackbarMessage"] = "Invalid student record!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(Index));
            }

            if (User.IsInRole("Student") && id != GetCurrentUserId())
            {
                TempData["SnackbarMessage"] = "You are not allowed to edit other students!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(MyProfile));
            }

            if (!ModelState.IsValid)
            {
                ViewData["SnackbarMessage"] = "Please correct the errors!";
                ViewData["SnackbarType"] = "error";
                return View(studentDetail);
            }

            var existing = await _firestore.GetStudentByIdAsync(id);
            if (existing == null)
            {
                TempData["SnackbarMessage"] = "Student record does not exist!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(Index));
            }

            if (User.IsInRole("Student"))
                studentDetail.Email = User.Identity?.Name ?? existing.Email;

            studentDetail.UserId = id;
            await _firestore.UpdateStudentAsync(id, studentDetail);

            TempData["SnackbarMessage"] = "Student updated successfully!";
            TempData["SnackbarType"] = "success";

            return User.IsInRole("Student")
                ? RedirectToAction(nameof(MyProfile))
                : RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["SnackbarMessage"] = "Student not found!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(Index));
            }

            var student = await _firestore.GetStudentByIdAsync(id);
            if (student == null)
            {
                TempData["SnackbarMessage"] = "Student record does not exist!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["SnackbarMessage"] = "Student not found!";
                TempData["SnackbarType"] = "error";
                return RedirectToAction(nameof(Index));
            }

            var student = await _firestore.GetStudentByIdAsync(id);
            if (student == null)
            {
                TempData["SnackbarMessage"] = "Student already deleted!";
                TempData["SnackbarType"] = "warning";
                return RedirectToAction(nameof(Index));
            }

            await _firestore.DeleteStudentAsync(id);

            TempData["SnackbarMessage"] = "Student deleted successfully!";
            TempData["SnackbarType"] = "success";

            return RedirectToAction(nameof(Index));
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)
                   ?? throw new InvalidOperationException("Authenticated Firebase user ID is missing.");
        }
    }
}
