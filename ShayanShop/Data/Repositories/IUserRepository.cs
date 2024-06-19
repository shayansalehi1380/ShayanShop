using ShayanShop.Models;
using System.Linq;

namespace ShayanShop.Data.Repositories
{
    public interface IUserRepository
    {
        bool IsExistByEmail(string email);
        void AddUser(Users users);
        Users GetUserForLogin(string email, string password);
    }

    public class UserRepository : IUserRepository
    {
        private ShayanShopContext _context;
        public UserRepository(ShayanShopContext context)
        {
            _context = context;
        }

        public void AddUser(Users users)
        {
            _context.Users.Add(users);
            _context.SaveChanges();
        }

        public bool IsExistByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }


        public Users GetUserForLogin(string email, string password)
        {
           return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
