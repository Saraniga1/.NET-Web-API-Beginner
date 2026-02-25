using FirstAPI.Data;
using FirstAPI.Models;

namespace FirstAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }   

        public IEnumerable<Product> GetAll(string? nameFilter = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                query = query.Where(p => p.Name.Contains(nameFilter));
            }

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public Product? Create(Product product)
        {
            if (product == null)
                return null;
            if (string.IsNullOrEmpty(product.Name))
                return null;
            if (product.Price < 0)
                return null;

            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product? Update(int id, Product updatedProduct) 
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return null;
            if (string.IsNullOrEmpty(updatedProduct.Name))
                return null;
            if (updatedProduct.Price < 0)
                return null;

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            _context.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
                return false;

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
    }
}
