using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SMS.Data;
using SMS.Models;
using SMS.ViewModels;
using System.Threading.Tasks;

namespace SMS.Service
{
    public class UserService
    {
        User obj = new User();
        private readonly SMSDBContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(SMSDBContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<string> SaveChangeAsync(UserViewModel _obj, bool IsSave = true)
        {
            if (_obj == null)
            {
                throw new ArgumentNullException(nameof(_obj));
            }
            if (IsSave)
            {
                
                obj.UserName = _obj.UserName;
                obj.Designation = _obj.Designation;
                obj.UserId = _obj.UserId;
                obj.Password = _passwordHasher.HashPassword(null, _obj.Password); // Hash the password
                obj.Status = "1";
                _context.User.Add(obj);
                
            }
            else
            {
                _context.User.Update(obj);
            }

            try
            {
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                // Log the exception (ex.Message) here if needed
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> ChangePasswordAsync(string userId, string newPassword)
        {
            // Find the user by UserId
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return "User not found.";
            }

            // Update the user's password
            //user.Password = newPassword;
            user.Password = _passwordHasher.HashPassword(null, newPassword);

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                // Log the exception (ex.Message) here if needed
                return $"Error: {ex.Message}";
            }
        }
        public async Task<bool> VerifyPasswordAsync(string userId, string password)
        {
            // Find the user by UserId
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return false;
            }

            // Verify the password
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success;
        }

        public async Task<Tuple<List<User>, string>> GetAllUserAsync()
        {
            try
            {
                var users = await _context.User.ToListAsync();
                return new Tuple<List<User>, string>(users, "Success");
            }
            catch (Exception ex)
            {
                // Log the exception (ex.Message) here if needed
                return new Tuple<List<User>, string>(new List<User>(), $"Error: {ex.Message}");
            }
        }
    }
}

