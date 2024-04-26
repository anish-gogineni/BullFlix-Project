using AnishProject.Models;

namespace AnishProject.Repositories
{
    public interface IUserRepository
    {
        User GetUser(User user);
    }
}
