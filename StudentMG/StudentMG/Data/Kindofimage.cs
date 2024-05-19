using System;
using System.Collections.Generic;

namespace StudentMG.Data;

public partial class Kindofimage
{
    public int ImageId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<StudentImage> StudentImages { get; set; } = new List<StudentImage>();
}
