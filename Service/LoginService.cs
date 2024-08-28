//using Microsoft.AspNetCore.Identity;
//using SMS.Models;
//using SMS.Data;
//using SMS.ViewModels;
//using System.Linq;

//namespace SMS.Service
//{
//    public class LoginService
//    {
//        private readonly SMSDBContext _context;
//        private readonly PasswordHasher<User> _passwordHasher;
//        public LoginService(SMSDBContext context)
//        {
//            _context = context;
//            _passwordHasher = new PasswordHasher<User>();
//        }
//        public Tuple<List<User>, string> GetUserById(string userId, string password)
//        {
//            if (_context.User == null)
//            {
//                // Handle the case where the User DbSet is not initialized
//                return new Tuple<List<User>, string>(new List<User>(), "User DbSet is not initialized");
//            }

//            var userData = _context.User
//                .Where(u => u.UserId == userId && u.Password == password)
//                .ToList();
//            string message = userData.Count > 0 ? "User found" : "User not found";
//            return new Tuple<List<User>, string>(userData, message);
//        }

//    }
//}
using Microsoft.AspNetCore.Identity;
using SMS.Models;
using SMS.Data;
using System.Linq;

namespace SMS.Service
{
    public class LoginService
    {
        private readonly SMSDBContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public LoginService(SMSDBContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public Tuple<List<User>, string> GetUserById(string userId, string password)
        {
            if (_context.User == null)
            {
                return new Tuple<List<User>, string>(new List<User>(), "User DbSet is not initialized");
            }

            var userData = _context.User
                .Where(u => u.UserId == userId)
                .ToList();

            if (userData.Count == 0)
            {
                return new Tuple<List<User>, string>(new List<User>(), "User not found");
            }

            var user = userData.FirstOrDefault();
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Success)
            {
                return new Tuple<List<User>, string>(userData, "User found");
            }
            else
            {
                return new Tuple<List<User>, string>(new List<User>(), "Invalid password");
            }
        }
    }
}

