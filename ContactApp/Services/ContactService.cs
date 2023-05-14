using ContactApp.Db;
using ContactApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ContactApp.Services
{
    // contact service which is used to handle getting users from the database
    public class ContactService : IContactService
    {
        private readonly ContactAppDbContext _context;
        public ContactService(ContactAppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetSingleUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return null;

            return user;
        }

        public async Task<User?> UpdateUser(int id, User request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return null;
            
            user.Username = request.Username;
            user.Surname = request.Surname;
            user.Email = request.Email;
            user.Password = request.Password;
            user.Category = request.Category;
            user.Phone = request.Phone;
            user.BirthDate = request.BirthDate;

            await _context.SaveChangesAsync();
            return user;
        }
    }
}
