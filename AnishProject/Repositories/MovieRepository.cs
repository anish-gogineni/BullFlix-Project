using AnishProject.Models;

namespace Anish_LookAndFeel.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private BullflixContext db;
        public MovieRepository(BullflixContext _db)
        {
            db = _db;
        }
        public Movie AddMovie(Movie student)
        {
            db.Movies.Add(student);
            db.SaveChanges();
            return student;
        }

        public bool DeleteMovie(int movieId)
        {
            var movie = db.Movies.SingleOrDefault(x => x.MovieId == movieId);
            if (movie != null)
            {
                db.Movies.Remove(movie);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Movie GetMovie(int id)
        {
            var movie = db.Movies.SingleOrDefault(x => x.MovieId == id);
            return movie;
        }

        public List<Movie> MoviesList()
        {
            return db.Movies.ToList();
        }

        public bool Update(Movie movie)
        {
            var movieData = db.Movies.SingleOrDefault(x => x.MovieId == movie.MovieId);
            if (movieData != null)
            {
                movieData.Title = movie.Title;
                movieData.GenreId = movie.GenreId;
                movieData.Description = movie.Description;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<MovieReview> GetMovieReviews(int id)
        {
            return db.MovieReviews.Where(x => x.MovieId == id).ToList();
        }

        public MovieReview AddMovieReview(MovieReview movieReview)
        {
            db.MovieReviews.Add(movieReview);
            db.SaveChanges();
            return movieReview;
        }
        public bool DeleteMovieReview(int id)
        {
            var movieReview = db.MovieReviews.SingleOrDefault(x => x.ReviewId == id);
            if (movieReview != null)
            {
                db.MovieReviews.Remove(movieReview);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}