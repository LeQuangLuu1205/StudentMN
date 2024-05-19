using System;
using System.Collections.Generic;

namespace StudentMG.Data;

public partial class StudentImage
{
    public string StudentId { get; set; } = null!;

    public int ImageId { get; set; }

    public string? Source { get; set; }

    public virtual Kindofimage Image { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
