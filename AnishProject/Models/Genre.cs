using System;
using System.Collections.Generic;

namespace AnishProject.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Documentary> Documentaries { get; } = new List<Documentary>();

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();

    public virtual ICollection<TvShow> TvShows { get; } = new List<TvShow>();
}
