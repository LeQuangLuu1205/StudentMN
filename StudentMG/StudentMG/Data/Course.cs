using System;
using System.Collections.Generic;

namespace StudentMG.Data;

public partial class Course
{
    public string CourseId { get; set; } = null!;

    public string? Name { get; set; }

    public int? NoCredits { get; set; }

    public virtual ICollection<Coursedetail> Coursedetails { get; set; } = new List<Coursedetail>();
}
