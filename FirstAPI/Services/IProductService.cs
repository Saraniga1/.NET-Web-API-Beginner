using FirstAPI.Models;

namespace FirstAPI.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll(string? nameFilter = null, int pageNumber = 1, int pageSize = 10);
        Product? GetById(int id);
        Product? Create(Product product);
        Product? Update(int id, Product product);
        bool Delete(int id);

    }
}
