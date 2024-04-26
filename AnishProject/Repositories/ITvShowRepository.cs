using AnishProject.Models;

namespace Anish_LookAndFeel.Repositories
{
    public interface ITvShowRepository
    {
        TvShow AddTvShow(TvShow tvShow);
        bool DeleteTvShow(int tvShowId);
        TvShow GetTvShow(int id);
        List<TvShow> TvShowsList();
        bool Update(TvShow tvShow);
        List<TvShowReview> GetTvReviews(int id);
        TvShowReview AddTvShowReview(TvShowReview tvShowReview);
        bool DeleteTvShowReview(int id);
    }
}
