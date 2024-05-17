using System;
using System.Collections.Generic;

namespace StudentMG.Data;

public partial class Coursedetail
{
    public int CourseDetailId { get; set; }

    public string? LecturerId { get; set; }

    public string? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();

    public virtual Lecturer? Lecturer { get; set; }
}
