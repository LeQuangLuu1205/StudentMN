using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StudentMG.Data;
using StudentMG.Helpers;
using StudentMG.ViewModels;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

namespace StudentMG.Controllers
{
    public class StudentController : Controller
    {
        private readonly DbStudentmanagementContext db;
        public StudentController(DbStudentmanagementContext context)
        {
            db = context;
        }

        #region Login
         [HttpGet]
         public IActionResult Login(string? ReturnUrl)
         {
             ViewBag.ReturnUrl = ReturnUrl;
             return View();
         }
         [HttpPost]
         public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl)
         {
             ViewBag.ReturnUrl = ReturnUrl;
             if (ModelState.IsValid)
             {
                 var student = db.Students.SingleOrDefault(st => st.Username == model.Username);
                 if (student == null)
                 {
                     ModelState.AddModelError("loi", "Không có sinh viên này");
                 }
                 else
                 {
                     if (!student.IsExist)
                     {
                         ModelState.AddModelError("loi", "Tài khoản đã bị khóa. Vui lòng liên hệ Admin!");
                     }
                     else
                     {
                         if (student.Password != model.Password.ToMd5Hash(student.RandomKey))   
                         {
                             ModelState.AddModelError("loi", "Sai thông tin đăng nhập!");
                         }
                         else
                         {
                             var claims = new List<Claim> {
                                 new Claim(ClaimTypes.Email, student.Email),
                                 new Claim(ClaimTypes.Name, student.Fullname),
                                 new Claim("MaSinhVien", student.StudentId),
                                 //claim - role động
                                 new Claim(ClaimTypes.Role, "Student")
                             };
                             var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                             var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                             await HttpContext.SignInAsync(claimsPrincipal);

                             if (Url.IsLocalUrl(ReturnUrl))
                             {
                                 return Redirect(ReturnUrl);
                             } 
                             else
                             {
                                 return Redirect("/");
                             }
                         }
                     }
                 }
             }
             return View();
         }
        #endregion

        #region Logout
        [Authorize]
		public IActionResult Profile()
         {

             return View();
         }

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
        #endregion

        #region Update Info
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            Student st = db.Students.Find(id);
            
           StudentInfoVM studentEdit= new StudentInfoVM(
                st.StudentId,
                st.Fullname,st.Email,st.PhoneNumber,st.DoB,
                st.Address,st.NoIdentity);
         

            return View(studentEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Update(StudentInfoVM model) 
        {
            if (ModelState.IsValid)
            {
                Student st = db.Students.Find(model.StudentId);
                st.Fullname = model.Fullname;
                st.Email = model.Email;
                st.PhoneNumber = model.PhoneNumber;
                st.DoB = model.DoB;
                st.Address = model.Address;
                st.NoIdentity = model.NoIdentity;
                db.SaveChanges();
            }
            return RedirectToAction("Profile","Student");
        }
        #endregion
    }
}
