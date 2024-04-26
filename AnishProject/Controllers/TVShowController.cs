using Anish_LookAndFeel.Repositories;
using AnishProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnishProject.Controllers
{
    public class TVShowController : Controller
    {
        private readonly ITvShowRepository _tvShowRepository;
        private readonly BullflixContext _db;
        public TVShowController(ITvShowRepository tvShowRepository, BullflixContext db)
        {
            _tvShowRepository = tvShowRepository;
            _db = db;
        }
        public IActionResult ViewAll()
        {
            var tvShowsList = _tvShowRepository.TvShowsList();
            foreach (var tvShow in tvShowsList)
            {
                tvShow.Genre = _db.Genres.SingleOrDefault(x => x.GenreId == tvShow.GenreId);
                decimal sum = 0;
                var reviewList = _tvShowRepository.GetTvReviews(tvShow.TvShowId);
                foreach (var review in reviewList)
                {
                    sum = sum + (decimal)review.Rating;
                }
                if (reviewList.Any())
                    tvShow.Rating = Math.Round((sum / reviewList.Count), 1).ToString();
                else
                    tvShow.Rating = 0.ToString();
            }
            return View(tvShowsList);
        }
        [HttpGet]
        public IActionResult AddTVShow()
        {
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            return View();
        }
        [HttpPost]
        public IActionResult AddTVShow(TvShow id)
        {
            if (ModelState.IsValid)
            {
                TvShow tvShow = _tvShowRepository.AddTvShow(id);
                return RedirectToAction("ViewAll");
            }
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            return View();
        }
        public IActionResult EditTVShow(int id)
        {
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            TvShow tvShow = _tvShowRepository.GetTvShow(id);
            return View(tvShow);
        }
        [HttpPost]
        public IActionResult EditTVShow(TvShow tvShow)
        {
            if (ModelState.IsValid)
            {
                if (_tvShowRepository.Update(tvShow))
                {
                    return RedirectToAction("ViewAll");
                }
            }
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            return View();
        }
        public IActionResult Delete(int id)
        {
            if (_tvShowRepository.DeleteTvShow(id))
            {
                return RedirectToAction("ViewAll");
            }
            TempData["LoginError"] = "TvShow Could Not be Deleted";  
            return RedirectToAction("ViewAll", "TVShow");
        }
        public IActionResult DeleteTvShowReview(int id)
        {
            if (_tvShowRepository.DeleteTvShowReview(id))
                return RedirectToAction("SeeReviews");
            throw new NotImplementedException();
        }
        public IActionResult SeeReviews(int id)
        {
            var reviws = _tvShowRepository.GetTvReviews(id);
            foreach (var review in reviws)
            {
                review.TvShow = _db.TvShows.SingleOrDefault(x => x.TvShowId == review.TvShowId);
            }
            return View(reviws);
        }
        public IActionResult AddReview()
        {
            ViewBag.TvShows = new SelectList(_db.TvShows, "TvShowId", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult AddReview(TvShowReview id)
        {
            if (ModelState.IsValid)
            {
                TvShowReview tvShowReview = _tvShowRepository.AddTvShowReview(id);
                return RedirectToAction("SeeReviews", new { id = tvShowReview.TvShowId});
            }
            ViewBag.TvShows = new SelectList(_db.TvShows, "TvShowId", "Title");
            return RedirectToAction("SeeReviews");
        }
    }
}
