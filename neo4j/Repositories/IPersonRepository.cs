using neo4j.Models;

namespace neo4j.Repositories;

public interface IPersonRepository
{
    Task CreatePersonAsync(string name, int age);
    Task<List<Person>> GetAllPersonsAsync();
}