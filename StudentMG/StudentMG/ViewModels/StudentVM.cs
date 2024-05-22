using System.ComponentModel.DataAnnotations;
namespace StudentMG.ViewModels
{
    public class StudentVM
    {
        [Key]
        public string StudentId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Fullname { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public StudentVM(string studentId, string? username, string? password, string? fullname, string? email, string? phoneNumber)
        {
            StudentId = studentId;
            Username = username;
            Password = password;
            Fullname = fullname;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public StudentVM()
        {
        }
    }
}
