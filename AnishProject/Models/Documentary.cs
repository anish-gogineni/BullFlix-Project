using System;
using System.Collections.Generic;

namespace AnishProject.Models;

public partial class Documentary
{
    public int DocumentaryId { get; set; }

    public string? Title { get; set; }

    public int? GenreId { get; set; }

    public string? Description { get; set; }

    public string? Rating { get; set; }

    public virtual ICollection<DocumentaryReview> DocumentaryReviews { get; } = new List<DocumentaryReview>();

    public virtual Genre? Genre { get; set; }
}
