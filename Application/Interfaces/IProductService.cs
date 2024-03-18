using Domain.Entities;

namespace Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(string id);
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(string id, Product product);
    Task DeleteProductAsync(string id);
}