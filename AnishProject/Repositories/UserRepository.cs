using AnishProject.Models;

namespace AnishProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private BullflixContext db;
        public UserRepository(BullflixContext _db) 
        {
            db= _db;
        }

        public User GetUser(User user)
        {
            var userExists = db.Users.SingleOrDefault(x=>x.UserName==( user.UserName) && x.Password==(user.Password));
            return userExists;
        }
    }
}
