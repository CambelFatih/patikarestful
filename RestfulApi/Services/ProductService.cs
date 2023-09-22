// Services/ProductService.cs
using MyWebApi.Models;
using MyWebApi.Repositories;

namespace MyWebApi.Services
{
    public class ProductService
    {
        private readonly JsonProductRepository _repository;

        public ProductService(JsonProductRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Product> SearchProducts(string? name, string orderBy = "Id", bool ascending = true, int page = 1, int pageSize = 10)
        {
            var products = _repository.GetAll();

            // Filtreleme
            if (!string.IsNullOrEmpty(name))
                products = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            // Sıralama için özellik kontrolü
            var property = typeof(Product).GetProperty(orderBy);
            if (property == null)
                throw new ArgumentException($"'{orderBy}' is not a valid property for ordering.");

            products = ascending
                ? products.OrderBy(p => property.GetValue(p))
                : products.OrderByDescending(p => property.GetValue(p));

            // Sayfalama
            products = products.Skip((page - 1) * pageSize).Take(pageSize);

            return products;
        }
    }
}
