using AnishProject.Models;

namespace Anish_LookAndFeel.Repositories
{
    public interface IDocumentaryRepository
    {
        Documentary AddDocumentary(Documentary documentary);
        bool DeleteDocumentary(int documentaryId);
        Documentary GetDocumentary(int id);
        List<Documentary> DocumentariesList();
        bool Update(Documentary documentary);
        List<DocumentaryReview> GetDocumentaryReviews(int id);
        DocumentaryReview AddDocumentaryReview(DocumentaryReview documentaryReview);
        bool DeleteDocumentaryReview(int id);
        }
}
