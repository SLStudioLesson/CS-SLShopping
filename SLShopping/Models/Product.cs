using System.ComponentModel.DataAnnotations;

namespace SLShopping.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Description { get; set; }
        public float Height { get; set; }
        [StringLength(255)]
        public string? Image {  get; set; }
        public float Length { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public int Price { get; set; }
        public float Weight { get; set; }
        public float Width { get; set; }
        public Brand? Brand { get; set; } = null!;
        public Category? Category { get; set; } = null!;
    }
}
