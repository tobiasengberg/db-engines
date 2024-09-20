using mongodb.models;

namespace mongodb.repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(string id);
    Task CreateAsync(Product product);
}