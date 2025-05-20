using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartTutionHub.Data;
using SmartTutionHub.Models;
using SmartTutionHub.ViewModels;

namespace SmartTutionHub.Controllers
{
    public class ProfileController: Controller
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ProfileController(UserManager<ApplicationUser> userMgr, AppDbContext db, IWebHostEnvironment env)
        {
            _userMgr = userMgr;
            _db = db;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userMgr.GetUserAsync(User);
            if (user == null) return NotFound();

            var vm = new ProfileVm
            {
                FullName = user.FullName,
                ProfileImagePath = user.ProfileImagePath,
                Email = user.Email,
                Role = user.Role,
                MyClasses = user.Role == "Tutor"
                    ? _db.Courses.Where(c => c.TutorId == user.Id).ToList()
                    : _db.Bookings.Where(b => b.StudentId == user.Id)
                          .Select(b => b.Course)
                          .ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProfileVm vm)
        {
            var user = await _userMgr.GetUserAsync(User);
            if (user == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(vm.FullName))
                user.FullName = vm.FullName;

            // Handle image upload
            if (vm.ProfileImage != null && vm.ProfileImage.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);

                var fileName = $"{user.Id}_{Path.GetFileName(vm.ProfileImage.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ProfileImage.CopyToAsync(stream);
                }
                user.ProfileImagePath = $"/uploads/{fileName}";
            }

            await _userMgr.UpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}
