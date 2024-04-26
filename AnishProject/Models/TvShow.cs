using System;
using System.Collections.Generic;

namespace AnishProject.Models;

public partial class TvShow
{
    public int TvShowId { get; set; }

    public string? Title { get; set; }

    public int? GenreId { get; set; }

    public string? Description { get; set; }

    public string? Rating { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<TvShowReview> TvShowReviews { get; } = new List<TvShowReview>();
}
