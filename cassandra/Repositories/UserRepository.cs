using Cassandra;
using cassandra.Models;
using ISession = Cassandra.ISession;

namespace cassandra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ISession _session;

    public UserRepository(ISession session)
    {
        _session = session;
    }

    public async Task InsertUserAsync(Guid id, string name, int age)
    {
        var query = "INSERT INTO users (id, name, age) VALUES (?, ?, ?)";
        var statement = _session.Prepare(query);
        await _session.ExecuteAsync(statement.Bind(id, name, age));
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = new List<User>();
        var rs = await _session.ExecuteAsync(new SimpleStatement("SELECT * FROM users"));

        foreach (var row in rs)
        {
            users.Add(new User
            {
                Id = row.GetValue<Guid>("id"),
                Name = row.GetValue<string>("name"),
                Age = row.GetValue<int>("age")
            });
        }

        return users;
    }
}