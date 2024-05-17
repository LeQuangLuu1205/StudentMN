using System;
using System.Collections.Generic;

namespace StudentMG.Data;

public partial class Faculty
{
    public string FacultyId { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Lecturer> Lecturers { get; set; } = new List<Lecturer>();
}
