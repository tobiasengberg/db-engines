using Neo4j.Driver;
using neo4j.Models;

namespace neo4j.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IAsyncSession _session;

    public PersonRepository(IAsyncSession session)
    {
        _session = session;
    }

    public async Task CreatePersonAsync(string name, int age)
    {
        var query = @"
            CREATE (p:Person {Name: $name, Age: $age})
            RETURN p";
        var parameters = new { name, age };

        await _session.RunAsync(query, parameters);
    }

    public async Task<List<Person>> GetAllPersonsAsync()
    {
        var persons = new List<Person>();
        var result = await _session.RunAsync("MATCH (p:Person) RETURN p.Name AS Name, p.Age AS Age");

        await result.ForEachAsync(record =>
        {
            var person = new Person
            {
                Name = record["Name"].As<string>(),
                Age = record["Age"].As<int>()
            };
            persons.Add(person);
        });

        return persons;
    }
}