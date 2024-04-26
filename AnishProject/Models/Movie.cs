using System;
using System.Collections.Generic;

namespace AnishProject.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? Title { get; set; }

    public int? GenreId { get; set; }

    public string? Description { get; set; }

    public string? Rating { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<MovieReview> MovieReviews { get; } = new List<MovieReview>();
}
