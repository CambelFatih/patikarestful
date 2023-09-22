using MyWebApi.Models;

public interface IProductService
{
    IEnumerable<Product> SearchProducts(string? name, string orderBy = "Id", bool ascending = true, int page = 1, int pageSize = 10);
}
