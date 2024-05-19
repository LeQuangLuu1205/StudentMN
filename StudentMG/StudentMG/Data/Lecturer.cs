using System;
using System.Collections.Generic;

namespace StudentMG.Data;

public partial class Lecturer
{
    public string LecturerId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateOnly? DoB { get; set; }

    public string? Image { get; set; }

    public bool? Gender { get; set; }

    public string? Address { get; set; }

    public string? NoIdentity { get; set; }

    public string? AcademicDegree { get; set; }

    public int Role { get; set; }

    public bool IsExist { get; set; }

    public string? RandomKey { get; set; }

    public string? FacultyId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Coursedetail> Coursedetails { get; set; } = new List<Coursedetail>();

    public virtual Faculty? Faculty { get; set; }
}
