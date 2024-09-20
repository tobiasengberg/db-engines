using Microsoft.AspNetCore.Mvc;
using mongodb.models;
using mongodb.repositories;

namespace mongodb.controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productRepository.GetAllAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(string name, decimal price)
    {
        await _productRepository.CreateAsync(new Product(name, price));
        return Ok("Product created");
    }
}