// Repositories/JsonProductRepository.cs
using MyWebApi.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;
namespace MyWebApi.Repositories
{
    // This class is responsible for providing CRUD operations on Products
    // which are stored in a JSON file. 
    public class JsonProductRepository
    {
        // Specifies the path of the JSON file where products are stored.
        private const string FilePath = "./Data/products.json";
        private IList<Product> products;

        // Constructor: Initializes the product list from the JSON file
        // or creates a new list if the file doesn't exist.
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

        // Retrieves all products from the list.
        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        // Fetches a product based on its ID.
        public Product GetById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        // Creates a new product, assigns it a unique ID, 
        // adds it to the list and saves the changes to the JSON file.
        public Product Create(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            SaveChanges();
            return product;
        }

        // Updates an existing product and saves the changes to the JSON file.
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

        // Deletes a product based on its ID and saves the changes to the JSON file.
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                products.Remove(product);
                SaveChanges();
            }
        }

        // Serializes the product list and writes it to the JSON file.
        private void SaveChanges()
        {
            var content = JsonConvert.SerializeObject(products);
            File.WriteAllText(FilePath, content);
        }

        // Fetches products based on a name filter.
        // If the name is null or empty, all products are returned.
        public IEnumerable<Product> GetFilteredProducts(string? name)
        {
            var products = GetAll();

            if (string.IsNullOrEmpty(name))
                return products;

            return products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        // Applies changes from the patch document to the specified product.
        public void ApplyPatch(int id, JsonPatchDocument<Product> patchDoc)
        {
            var product = GetById(id);
            if (product != null)
            {
                patchDoc.ApplyTo(product);  // Exception handling should be done here or higher layers.
                SaveChanges();
            }
        }
    }
}
