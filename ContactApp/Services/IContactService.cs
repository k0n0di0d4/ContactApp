using ContactApp.Models;

namespace ContactApp.Services
{
    public interface IContactService
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetSingleUser(int id);

        Task<User?> UpdateUser(int id, User request);

        Task<User?> DeleteUser(int id);
    }
}
