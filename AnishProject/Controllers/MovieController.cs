using Anish_LookAndFeel.Repositories;
using AnishProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace AnishProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly BullflixContext _db;
        public MovieController(IMovieRepository movieRepository, BullflixContext db)
        {
            _movieRepository = movieRepository;
            _db = db;
        }
        public IActionResult ViewAll()
        {
            var moviesList = _movieRepository.MoviesList();
            foreach(var movie in moviesList)
            {
                movie.Genre = _db.Genres.SingleOrDefault(x=>x.GenreId== movie.GenreId);
                decimal sum = 0;
                var reviewList = _movieRepository.GetMovieReviews(movie.MovieId);
                foreach(var review in reviewList)
                {
                    sum = sum + (decimal)review.Rating;
                }
                if (reviewList.Any())
                    movie.Rating = Math.Round((sum / reviewList.Count),1).ToString();
                else
                    movie.Rating = 0.ToString();
            }
            return View(moviesList);
        }
        [HttpGet]
        public IActionResult AddMovie()
        {
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            return View();
        }
        [HttpPost]
        public IActionResult AddMovie(Movie id)
        {
            if (ModelState.IsValid)
            {
                Movie movie = _movieRepository.AddMovie(id);
                return RedirectToAction("ViewAll");
            }
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EditMovie(int id)
        {
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            Movie movie = _movieRepository.GetMovie(id);
            return View(movie);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (_movieRepository.Update(movie))
                {
                    return RedirectToAction("ViewAll");
                }
            }
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            if (_movieRepository.DeleteMovie(id))
            {
                return RedirectToAction("ViewAll");
            }
            TempData["LoginError"] = "Movie could not be deleted";
            return RedirectToAction("ViewAll", "Movie");
        }
        public IActionResult DeleteMovieReview(int id)
        {
            if (_movieRepository.DeleteMovieReview(id))
                return RedirectToAction("SeeReviews");
            throw new NotImplementedException();
        }
        public IActionResult SeeReviews(int id)
            {
                var reviws = _movieRepository.GetMovieReviews(id);
                foreach(var review in reviws)
                {
                    review.Movie = _db.Movies.SingleOrDefault(x => x.MovieId == review.MovieId);
                }
                return View(reviws);
            }
        public IActionResult AddReview()
        {

            ViewBag.Movies = new SelectList(_db.Movies, "MovieId", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult AddReview(MovieReview id)
        {
            if (ModelState.IsValid)
            {
                MovieReview movieReview = _movieRepository.AddMovieReview(id);
                return RedirectToAction("SeeReviews",new {id = movieReview.MovieId});
            }
            ViewBag.Movies = new SelectList(_db.Movies, "MovieId", "Title");
            return RedirectToAction("SeeReviews");
        }
    }
}
