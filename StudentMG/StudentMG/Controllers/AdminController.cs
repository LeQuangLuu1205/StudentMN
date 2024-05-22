using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StudentMG.Data;
using StudentMG.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentMG.Helpers;

namespace StudentMG.Controllers
{
    public class AdminController : Controller
    {
        private readonly DbStudentmanagementContext db;
        public AdminController(DbStudentmanagementContext context)
        {
            db = context;

        }
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var acc = db.AccountAdmins.SingleOrDefault(ad => ad.Username == model.Username && ad.Password== model.Password);
                if (acc == null)
                {
                    ModelState.AddModelError("loi", "Không có tài khỏan này");  
                }
                else
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
            }
                return View();
        }
        #endregion

        #region Dashboard()
        public IActionResult Dashboard()
        {
            return View();
        }
        #endregion

        #region Show list student
        public IActionResult ShowStudentList()
        {
            var list = db.Students.ToList();
            var list2 = list.Select(s => new StudentVM
            {
                StudentId = s.StudentId,
                Username = s.Username,
                Password = s.Password,
                Fullname = s.Fullname,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber
            }).ToList();
            return View(list2);
           
        }
        #endregion create a student
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new Student2VM
            {
                Classes = db.Classes.Select(c => new SelectListItem
                {
                    Value = c.ClassId,
                    Text = c.Name
                }).ToList()
            };

            return View(viewModel);
           
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student2VM model)
        {
           

            if (ModelState.IsValid)
            {
                model.Student.RandomKey = MyUtil.GenerateRandomkey();
                model.Student.Password = model.Student.Password.ToMd5Hash(model.Student.RandomKey);
                db.Students.Add(model.Student);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu có lỗi, nạp lại danh sách Classes
            model.Classes = db.Classes.Select(c => new SelectListItem
            {
                Value = c.ClassId,
                Text = c.Name
            }).ToList();

            return RedirectToAction("ShowStudentList","Admin");
        }
    }
}
