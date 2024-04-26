using System;
using System.Collections.Generic;

namespace AnishProject.Models;

public partial class MovieReview
{
    public int ReviewId { get; set; }

    public int? MovieId { get; set; }

    public string? UserName { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public virtual Movie? Movie { get; set; }
}
