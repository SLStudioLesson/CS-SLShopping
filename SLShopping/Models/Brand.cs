using System.ComponentModel.DataAnnotations;

namespace SLShopping.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        [StringLength(45)]
        public string Name { get; set; }
        public List<Product> Products { get; set; } = [];
    }
}
