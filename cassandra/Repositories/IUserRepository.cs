using cassandra.Models;

namespace cassandra.Repositories;

public interface IUserRepository
{
    Task InsertUserAsync(Guid id, string name, int age);
    Task<List<User>> GetAllUsersAsync();
}