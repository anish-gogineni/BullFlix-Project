using AnishProject.Models;

namespace Anish_LookAndFeel.Repositories
{
    public interface IMovieRepository
    {
        Movie AddMovie(Movie student);
        bool DeleteMovie(int movieId);
        Movie GetMovie(int id);
        List<Movie> MoviesList();
        bool Update(Movie movie);
        List<MovieReview> GetMovieReviews(int id);
        MovieReview AddMovieReview(MovieReview movieReview);
        bool DeleteMovieReview(int id);
    }
}
