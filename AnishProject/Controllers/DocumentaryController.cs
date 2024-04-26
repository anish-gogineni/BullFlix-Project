using Anish_LookAndFeel.Repositories;
using AnishProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnishProject.Controllers
{
    public class DocumentaryController : Controller
    {
        private readonly IDocumentaryRepository _documentaryRepository;
        private readonly BullflixContext _db;
        public DocumentaryController(IDocumentaryRepository documentaryRepository, BullflixContext db)
        {
            _documentaryRepository = documentaryRepository;
            _db = db;
        }
        public IActionResult ViewAll()
        {
            var documentariesList = _documentaryRepository.DocumentariesList();
            foreach (var documentary in documentariesList)
            {
                documentary.Genre = _db.Genres.SingleOrDefault(x => x.GenreId == documentary.GenreId);
                decimal sum = 0;
                var reviewList = _documentaryRepository.GetDocumentaryReviews(documentary.DocumentaryId);
                foreach (var review in reviewList)
                {
                    sum = sum + (decimal)review.Rating;
                }
                if (reviewList.Any())
                    documentary.Rating = Math.Round((sum / reviewList.Count), 1).ToString();
                else
                    documentary.Rating = 0.ToString();
            }
            return View(documentariesList);
        }
        [HttpGet]
        public IActionResult AddDocumentary()
        {
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");

            return View();
        }
        [HttpPost]
        public IActionResult AddDocumentary(Documentary id)
        {
            if (ModelState.IsValid)
            {
                Documentary documentary = _documentaryRepository.AddDocumentary(id);
                return RedirectToAction("ViewAll");
            }
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            return View();
        }
        public IActionResult EditDocumentary(int id)
        {
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            Documentary documentary = _documentaryRepository.GetDocumentary(id);
            return View(documentary);
        }
        [HttpPost]
        public IActionResult EditDocumentary(Documentary documentary)
        {
            if (ModelState.IsValid)
            {
                if (_documentaryRepository.Update(documentary))
                {
                    return RedirectToAction("ViewAll");
                }
            }
            ViewBag.Genres = new SelectList(_db.Genres, "GenreId", "GenreName");
            return View();
        }
        public IActionResult Delete(int id)
        {
            if (_documentaryRepository.DeleteDocumentary(id))
            {
                return RedirectToAction("ViewAll");
            }
            TempData["LoginError"] = "Documentary could not be deleted";
            return RedirectToAction("ViewAll", "Docuementary");
        }
        public IActionResult DeleteDocumentaryReview(int id)
        {
            if (_documentaryRepository.DeleteDocumentaryReview(id))
                return RedirectToAction("SeeReviews");
            throw new NotImplementedException();
        }
        public IActionResult SeeReviews(int id)
        {
            var reviws = _documentaryRepository.GetDocumentaryReviews(id);
            foreach (var review in reviws)
            {
                review.Documentary = _db.Documentaries.SingleOrDefault(x => x.DocumentaryId == review.DocumentaryId);
            }
            return View(reviws);
        }
        public IActionResult AddReview()
        {
            ViewBag.Documentaries = new SelectList(_db.Documentaries, "DocumentaryId", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult AddReview(DocumentaryReview id)
        {
            if (ModelState.IsValid)
            {
                DocumentaryReview documentaryReview = _documentaryRepository.AddDocumentaryReview(id);
                return RedirectToAction("SeeReviews", new { id = documentaryReview.DocumentaryId });
            }
            ViewBag.Documentaries = new SelectList(_db.Documentaries, "DocumentaryId", "Title");
            return RedirectToAction("SeeReviews");
        }
    }
}
