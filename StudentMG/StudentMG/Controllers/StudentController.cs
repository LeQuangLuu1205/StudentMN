using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using Microsoft.EntityFrameworkCore;
using StudentMG.Data;
using StudentMG.Helpers;
using StudentMG.ViewModels;
using System.Security.Claims;

namespace StudentMG.Controllers
{
    public class StudentController : Controller
    {
        private readonly DbStudentmanagementContext db;
        private readonly IWebHostEnvironment environment;
        public StudentController(DbStudentmanagementContext context, IWebHostEnvironment environment)
        {
            db = context;
            this.environment = environment;
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

            StudentInfoVM studentEdit = new StudentInfoVM(
                 st.StudentId,
                 st.Fullname, st.Email, st.PhoneNumber, st.DoB,
                 st.Address, st.NoIdentity);


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
            return RedirectToAction("Profile", "Student");
        }
        #endregion

        #region show grade
        public async Task<IActionResult> ShowGrade(string searchString)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var maSinhVienClaim = identity.FindFirst("MaSinhVien");
            var maSinhVien = maSinhVienClaim != null ? maSinhVienClaim.Value : "Not Available";
            List<StudentGradeVM> studentGrades;


            if (!String.IsNullOrEmpty(searchString))
            {
                studentGrades = (from cs in db.CourseStudents
                                 join cd in db.Coursedetails on cs.CourseDetailId equals cd.CourseDetailId
                                 join c in db.Courses on cd.CourseId equals c.CourseId
                                 where cs.StudentId == maSinhVien && c.Name.Contains(searchString)
                                 select new StudentGradeVM
                                 {
                                     Name = c.Name,
                                     NoCredits = c.NoCredits,
                                     Grade = cs.Grade
                                 }).ToList();
            }
            else
            {
                studentGrades = (from cs in db.CourseStudents
                                 join cd in db.Coursedetails on cs.CourseDetailId equals cd.CourseDetailId
                                 join c in db.Courses on cd.CourseId equals c.CourseId
                                 where cs.StudentId == maSinhVien
                                 select new StudentGradeVM
                                 {
                                     Name = c.Name,
                                     NoCredits = c.NoCredits,
                                     Grade = cs.Grade
                                 }).ToList();
            }

            var list = studentGrades
            .Select((grade, index) => new GradeVM
            {
                No = index + 1,
                Name = grade.Name,
                NoCredits = grade.NoCredits,
                Grade = grade.Grade
            }).ToList();

            return View(list);
        }
        #endregion

        #region image managemnet
        public async Task<IActionResult> ShowListImage()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var maSinhVienClaim = identity.FindFirst("MaSinhVien");
            var maSinhVien = maSinhVienClaim != null ? maSinhVienClaim.Value : "Not Available";
            List<ImageTempVM> imagelist;
            imagelist = (from si in db.StudentImages
                            join k in db.Kindofimages on si.ImageId equals k.ImageId
                            where si.StudentId == maSinhVien
                            select new ImageTempVM 
                            {
                                Name = k.Name,
                                Source = si.Source
                            }).ToList();
            var list = imagelist
            .Select((image, index) => new ImageVM
            {
                No = index + 1,
                Name = image.Name,
                Source = image.Source,
            }).ToList();
            return View(list);
        }

        #endregion

        #region edit image
        public async Task<IActionResult> EditImage(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var maSinhVienClaim = identity.FindFirst("MaSinhVien");
            var maSinhVien = maSinhVienClaim != null ? maSinhVienClaim.Value : "Not Available";
            List<ImageTempVM> imagelist;
            imagelist = (from si in db.StudentImages
                         join k in db.Kindofimages on si.ImageId equals k.ImageId
                         where si.StudentId == maSinhVien
                         select new ImageTempVM
                         {
                             Name = k.Name,
                             Source = si.Source
                         }).ToList();
            var list = imagelist
            .Select((image, index) => new ImageVM
            {
                No = index + 1,
                Name = image.Name,
                Source = image.Source,
            }).ToList();
            var data = list.FirstOrDefault(image => image.No == id);

                return View(data);
        }
        
        [HttpPost]
        public IActionResult EditImage(ImageVM model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var maSinhVienClaim = identity.FindFirst("MaSinhVien");
            var maSinhVien = maSinhVienClaim != null ? maSinhVienClaim.Value : "Not Available";
            var data = db.StudentImages.Where(e => e.StudentId == maSinhVien && e.ImageId == model.No).SingleOrDefault();
            string uniqueFileName = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Source!=null)
                    {
                        string filepath = Path.Combine(environment.WebRootPath, "assets/img/info", data.Source);
                        if (System.IO.File.Exists(filepath))
                        {
                            System.IO.File.Delete(filepath);
                        }
                        string uploadFolder = Path.Combine(environment.WebRootPath, "assets/img/info/");
                        uniqueFileName = Guid.NewGuid().ToString()+ "_"+model.Source;
                        String filepath2 = Path.Combine(uploadFolder, uniqueFileName);
                        var fileStream = new FileStream(filepath2, FileMode.Create);
                        
                    }
                    data.Source = model.Source;
                    data.StudentId = maSinhVien;
                    data.ImageId = model.No;
                    db.StudentImages.Update(data);
                    db.SaveChanges();
                }
            } catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("ShowListImage","Student");
        }
        #endregion

    }
}
