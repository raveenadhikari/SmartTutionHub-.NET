
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartTutionHub.Data;
using SmartTutionHub.Models;
using SmartTutionHub.ViewModels;


namespace SmartTutionHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public HomeController(UserManager<ApplicationUser> userMgr, AppDbContext db, IWebHostEnvironment env)
        {
            _userMgr = userMgr;
            _db = db;
            _env = env;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction(nameof(Dashboard));
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            // Fetch the logged-in user
            var user = await _userMgr.GetUserAsync(User);
            if (user == null) return Challenge();
            var myClasses = _db.Classes.Where(c => c.TutorId == user.Id).ToList();

            // If you're using a ViewModel, put classes in it. Or, use ViewBag:
            ViewBag.MyClasses = myClasses;

            

            // Pass the full user object (or just the name) into the view
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddClass(AddClassVm vm)
        {
            var user = await _userMgr.GetUserAsync(User);
            if (user == null || user.Role != "Tutor")
                return Unauthorized();

            if (!ModelState.IsValid)
            {
                ViewBag.MyClasses = _db.Classes.Where(c => c.TutorId == user.Id).ToList();
                return View("Dashboard", user);
            }

            // File uploads
            string thumbnailPath = null;
            var galleryPaths = new List<string>();

            if (vm.Thumbnail != null && vm.Thumbnail.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);

                var fileName = $"thumb_{Guid.NewGuid()}_{Path.GetFileName(vm.Thumbnail.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.Thumbnail.CopyToAsync(stream);
                }
                thumbnailPath = $"/uploads/{fileName}";
            }

            if (vm.Photos != null && vm.Photos.Any())
            {
                foreach (var photo in vm.Photos)
                {
                    var uploads = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploads);
                    var fileName = $"gallery_{Guid.NewGuid()}_{Path.GetFileName(photo.FileName)}";
                    var filePath = Path.Combine(uploads, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }
                    galleryPaths.Add($"/uploads/{fileName}");
                }
            }

            // Save class to Classes table
            var newClass = new Class
            {
                Level = vm.Level,
                TutorName = vm.TutorName,
                Qualification = vm.Qualification,
                Subject = vm.Subject,
                GradeOrYear = vm.GradeOrYear,
                Medium = vm.Medium,
                ClassType = vm.ClassType,
                ClassTime = vm.ClassTime,
                ToBeDiscussed = vm.ToBeDiscussed,
                Description = vm.Description,
                ThumbnailPath = thumbnailPath,
                GalleryPaths = string.Join(',', galleryPaths),
                TutorId = user.Id,
                Tutor = user,
                CreatedAt = DateTime.Now
            };

            _db.Classes.Add(newClass);
            await _db.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }
    }
}
