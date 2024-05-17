using System;
using System.Collections.Generic;

namespace StudentMG.Data;

public partial class CourseStudent
{
    public int CourseDetailId { get; set; }

    public string StudentId { get; set; } = null!;

    public int? Grade { get; set; }

    public virtual Coursedetail CourseDetail { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
