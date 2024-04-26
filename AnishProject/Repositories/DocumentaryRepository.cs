using AnishProject.Models;

namespace Anish_LookAndFeel.Repositories
{
    public class DocumentaryRepository : IDocumentaryRepository
    {
        private BullflixContext db;
        public DocumentaryRepository(BullflixContext _db)
        {
            db = _db;
        }
        public Documentary AddDocumentary(Documentary documentary)
        {
            db.Documentaries.Add(documentary);
            db.SaveChanges();
            return documentary;
        }

        public bool DeleteDocumentary(int documentaryId)
        {
            var documentary = db.Documentaries.SingleOrDefault(x => x.DocumentaryId == documentaryId);
            if (documentary != null)
            {
                db.Documentaries.Remove(documentary);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Documentary GetDocumentary(int id)
        {
            var documentary = db.Documentaries.SingleOrDefault(x => x.DocumentaryId == id);
            return documentary;
        }

        public List<Documentary> DocumentariesList()
        {
            return db.Documentaries.ToList();
        }

        public bool Update(Documentary documentary)
        {
            var documentaryData = db.TvShows.SingleOrDefault(x => x.TvShowId == documentary.DocumentaryId);
            if (documentaryData != null)
            {
                documentaryData.Title = documentary.Title;
                documentaryData.GenreId = documentary.GenreId;
                documentaryData.Description = documentary.Description;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public List<DocumentaryReview> GetDocumentaryReviews(int id)
        {
            return db.DocumentaryReviews.Where(x => x.DocumentaryId == id).ToList();
        }
        public DocumentaryReview AddDocumentaryReview(DocumentaryReview documentaryReview)
        {
            db.DocumentaryReviews.Add(documentaryReview);
            db.SaveChanges();
            return documentaryReview;
        }
        public bool DeleteDocumentaryReview(int id)
        {
            var documentaryReview = db.DocumentaryReviews.SingleOrDefault(x => x.ReviewId == id);
            if (documentaryReview != null)
            {
                db.DocumentaryReviews.Remove(documentaryReview);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
