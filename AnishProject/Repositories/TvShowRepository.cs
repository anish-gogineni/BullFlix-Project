using AnishProject.Models;

namespace Anish_LookAndFeel.Repositories
{
    public class TvShowRepository : ITvShowRepository
    {
        private BullflixContext db;
        public TvShowRepository(BullflixContext _db)
        {
            db = _db;
        }
        public TvShow AddTvShow(TvShow tvShow)
        {
            db.TvShows.Add(tvShow);
            db.SaveChanges();
            return tvShow;
        }

        public bool DeleteTvShow(int tvShowId)
        {
            var tvShow = db.TvShows.SingleOrDefault(x => x.TvShowId == tvShowId);
            if (tvShow != null)
            {
                db.TvShows.Remove(tvShow);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public TvShow GetTvShow(int id)
        {
            var tvShow = db.TvShows.SingleOrDefault(x => x.TvShowId == id);
            return tvShow;
        }

        public List<TvShow> TvShowsList()
        {
            return db.TvShows.ToList();
        }

        public bool Update(TvShow tvShow)
        {
            var tvShowData = db.TvShows.SingleOrDefault(x => x.TvShowId == tvShow.TvShowId);
            if (tvShowData != null)
            {
                tvShowData.Title = tvShow.Title;
                tvShowData.GenreId = tvShow.GenreId;
                tvShowData.Description = tvShow.Description;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public List<TvShowReview> GetTvReviews(int id)
        {
            return db.TvShowReviews.Where(x => x.TvShowId == id).ToList();
        }
        public TvShowReview AddTvShowReview(TvShowReview tvShowReview)
        {
            db.TvShowReviews.Add(tvShowReview);
            db.SaveChanges();
            return tvShowReview;
        }
        public bool DeleteTvShowReview(int id)
        {
            var tvShowReview = db.TvShowReviews.SingleOrDefault(x => x.ReviewId == id);
            if (tvShowReview != null)
            {
                db.TvShowReviews.Remove(tvShowReview);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}