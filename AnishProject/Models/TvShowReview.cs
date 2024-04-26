using System;
using System.Collections.Generic;

namespace AnishProject.Models;

public partial class TvShowReview
{
    public int ReviewId { get; set; }

    public int? TvShowId { get; set; }

    public string? UserName { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public virtual TvShow? TvShow { get; set; }
}
