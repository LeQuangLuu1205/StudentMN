using System.ComponentModel.DataAnnotations;

namespace StudentMG.ViewModels
{
    public class StudentInfoVM
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "*")]
        public string StudentId { get; set; }


        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "*")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        public DateOnly? DoB { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "*")]
        public string Address { get; set; }

        [Display(Name = "CMND")]
        [Required(ErrorMessage = "*")]
        public string NoIdentity { get; set; }

        public StudentInfoVM(string studentId, string fullname, string email, string phoneNumber, DateOnly? DoB, string address, string noIdentity)
        {
            StudentId = studentId;
            Fullname = fullname;
            Email = email;
            PhoneNumber = phoneNumber;
            DoB = DoB;
            Address = address;
            NoIdentity = noIdentity;
        }

        public StudentInfoVM()
        {
        }
    }
}
