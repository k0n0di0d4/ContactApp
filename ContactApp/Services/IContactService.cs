using ContactApp.Models;

namespace ContactApp.Services
{
    // interface for our contact service which is used to handle getting users from the database
    public interface IContactService
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetSingleUser(int id);

        Task<User?> UpdateUser(int id, User request);

        Task<User?> DeleteUser(int id);
    }
}
