using System;
using System.Collections.Generic;

namespace StudentMG.Data;

public partial class Student
{
    public string StudentId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

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
}
