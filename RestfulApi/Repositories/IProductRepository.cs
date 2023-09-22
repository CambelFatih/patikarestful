// Repositories/IProductRepository.cs
using Microsoft.AspNetCore.JsonPatch;
using MyWebApi.Models;

namespace MyWebApi.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product Create(Product product);
        void Update(int id, Product product);
        void Delete(int id);
        void ApplyPatch(int id, JsonPatchDocument<Product> patchDoc);
    }
}
