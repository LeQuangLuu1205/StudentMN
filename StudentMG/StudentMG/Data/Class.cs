using System;
using System.Collections.Generic;

namespace StudentMG.Data;

public partial class Class
{
    public string ClassId { get; set; } = null!;

    public string? Name { get; set; }

    public string? LecturerId { get; set; }

    public virtual Lecturer? Lecturer { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
