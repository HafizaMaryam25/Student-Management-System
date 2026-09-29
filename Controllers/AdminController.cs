using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Services;

namespace StudentManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly FirestoreService _firestore;

        public AdminController(FirestoreService firestore)
        {
            _firestore = firestore;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _firestore.GetStudentsAsync());
        }
    }
}
