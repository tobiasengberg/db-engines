using MongoDB.Bson;
using MongoDB.Driver;
using mongodb.models;

namespace mongodb.repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(IMongoDatabase database)
    {
        _products = database.GetCollection<Product>("Products");
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _products.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(string id)
    {
        return await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Product product)
    {
        await _products.InsertOneAsync(product);
    }
}