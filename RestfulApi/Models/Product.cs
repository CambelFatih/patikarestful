using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public decimal Price { get; set; }
        
        public string? Category { get; set; }

        public int Stock { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}
