using System;
using System.Collections.Generic;

namespace AnishProject.Models;

public partial class DocumentaryReview
{
    public int ReviewId { get; set; }

    public int? DocumentaryId { get; set; }

    public string? UserName { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public virtual Documentary? Documentary { get; set; }
}
