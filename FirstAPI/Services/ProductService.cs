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

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList(); 
        }

        public Product? GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public Product Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public bool Update(int id, Product updatedProduct) 
        {
            var product = _context.Products.Find(id);

            if (product != null)
                return false;

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            _context.SaveChanges();
            return true;
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
