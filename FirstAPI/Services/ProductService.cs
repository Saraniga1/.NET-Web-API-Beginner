using FirstAPI.Models;

namespace FirstAPI.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> products = new();

        public IEnumerable<Product> GetAll()
        {
            return products; 
        }

        public Product? GetById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public Product Create(Product product)
        {
            product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
            products.Add(product);
            return product;
        }

        public bool Update(int id, Product updatedProduct) {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                return false;
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return true;
        }

        public bool Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                return false;
            products.Remove(product);
            return true;
        }
    }
}
