using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentMG.Data;
using StudentMG.Helpers;
using StudentMG.ViewModels;
using System.Security.Claims;

namespace StudentMG.Controllers
{
    public class StudentController : Controller
    {
        private readonly DbStudentmanagementContext db;
        public StudentController(DbStudentmanagementContext context)
        {
            db = context;
        }

         #region
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
	}
}
