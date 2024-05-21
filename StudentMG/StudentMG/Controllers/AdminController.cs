using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StudentMG.Data;
using StudentMG.ViewModels;
using System.Security.Claims;

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

        #region
        public IActionResult ShowStudentList()
        {
            return View();
        }
        #endregion
    }
}
