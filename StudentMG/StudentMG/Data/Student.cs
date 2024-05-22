using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
namespace StudentMG.Data;

public partial class Student
{
    [Required]
    public string StudentId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }
    [StringLength(50, MinimumLength = 10, ErrorMessage = "Họ tên phải có độ dài từ 10 đến 50 ký tự.")]
    public string? Fullname { get; set; }

    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    public string? Email { get; set; }

    [RegularExpression(@"^(090|098|091|031|035|038)\d{7}$", ErrorMessage = "Số điện thoại phải có độ dài 10 ký tự và bắt đầu bằng 090, 098, 091, 031, 035 hoặc 038.")]
    public string? PhoneNumber { get; set; }

    
    public DateOnly? DoB { get; set; }

    public bool? Gender { get; set; }

    public string? Address { get; set; }

    public string? NoIdentity { get; set; }

    public int Role { get; set; }

    public bool IsExist { get; set; }

    public string? RandomKey { get; set; }

    public string? ClassId { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();

    public virtual ICollection<StudentImage> StudentImages { get; set; } = new List<StudentImage>();

   
}
