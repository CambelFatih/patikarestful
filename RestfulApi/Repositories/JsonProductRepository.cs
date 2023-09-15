//Repositories/JsonProductRepository.cs
using MyWebApi.Models;
using Newtonsoft.Json;

namespace MyWebApi.Repositories
{
    public class JsonProductRepository
    {
        private const string FilePath = "./Data/products.json";
        private IList<Product> products;

        public JsonProductRepository()
        {
            if (File.Exists(FilePath))
            {
                var content = File.ReadAllText(FilePath);
                products = JsonConvert.DeserializeObject<List<Product>>(content);
            }
            else
            {
                products = new List<Product>();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product GetById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public Product Create(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            SaveChanges();
            return product;
        }

        public void Update(int id, Product product)
        {
            var existingProduct = GetById(id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Category = product.Category;
                existingProduct.Stock = product.Stock;
                existingProduct.DateAdded = product.DateAdded;
                SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                products.Remove(product);
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            var content = JsonConvert.SerializeObject(products);
            File.WriteAllText(FilePath, content);
        }
        // JsonProductRepository.cs içinde bu metodu ekleyin.
        public IEnumerable<Product> GetFilteredProducts(string? name)
        {
            var products = GetAll();

            if (string.IsNullOrEmpty(name))
                return products;

            return products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

    }
}
