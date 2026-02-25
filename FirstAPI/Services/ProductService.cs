using FirstAPI.Models;

namespace FirstAPI.Services
{
    public class ProductService : IProductService
    {
        private static readonly List<Product> products = [];

        public IEnumerable<Product> GetAll()
        {
            return products; 
        }

        public Product? GetById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public Product? Create(Product product)
        {
            if (product == null)
                return null;

            if (string.IsNullOrWhiteSpace(product.Name))
                return null;

            if (product.Price < 0)
                return null;

            product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
            products.Add(product);
            return product;
        }

        public Product? Update(int id, Product updatedProduct) 
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return null;

            if(string.IsNullOrWhiteSpace(updatedProduct.Name))
                return null;

            if (updatedProduct.Price < 0)
                return null;

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;

            return product;
        }

        public bool Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return false;

            products.Remove(product);
            return true;
        }
    }
}
