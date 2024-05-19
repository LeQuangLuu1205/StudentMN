using System.ComponentModel.DataAnnotations;

namespace StudentMG.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Tên đăng nhâp")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
