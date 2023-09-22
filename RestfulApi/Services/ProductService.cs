using MyWebApi.Models;
using MyWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Searches for products based on the given criteria.
        /// </summary>
        /// <param name="name">The name to filter by.</param>
        /// <param name="orderBy">The property to order by.</param>
        /// <param name="ascending">Indicates if the ordering should be ascending or descending.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <returns>A filtered, sorted, and paginated list of products.</returns>
        public IEnumerable<Product> SearchProducts(string? name, string orderBy = "Id", bool ascending = true, int page = 1, int pageSize = 10)
        {
            var products = _repository.GetAll();
            products = FilterProductsByName(products, name);
            products = SortProducts(products, orderBy, ascending);
            return PaginateProducts(products, page, pageSize);
        }

        /// <summary>
        /// Filters products by name.
        /// </summary>
        /// <param name="products">The list of products to filter.</param>
        /// <param name="name">The name to filter by.</param>
        /// <returns>A filtered list of products.</returns>
        private IEnumerable<Product> FilterProductsByName(IEnumerable<Product> products, string? name)
        {
            if (!string.IsNullOrEmpty(name))
                return products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            return products;
        }

        /// <summary>
        /// Sorts the products by a given property.
        /// </summary>
        /// <param name="products">The list of products to sort.</param>
        /// <param name="orderBy">The property to order by.</param>
        /// <param name="ascending">Indicates if the ordering should be ascending or descending.</param>
        /// <returns>A sorted list of products.</returns>
        private IEnumerable<Product> SortProducts(IEnumerable<Product> products, string orderBy, bool ascending)
        {
            var property = typeof(Product).GetProperty(orderBy);
            if (property == null)
                throw new ArgumentException($"'{orderBy}' is not a valid property for ordering.");

            return ascending
                ? products.OrderBy(p => property.GetValue(p))
                : products.OrderByDescending(p => property.GetValue(p));
        }

        /// <summary>
        /// Paginates the products list.
        /// </summary>
        /// <param name="products">The list of products to paginate.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <returns>A paginated list of products.</returns>
        private IEnumerable<Product> PaginateProducts(IEnumerable<Product> products, int page, int pageSize)
        {
            return products.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
