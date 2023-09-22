using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        public decimal Price { get; set; }

        public string? Category { get; set; }

        public int Stock { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}
