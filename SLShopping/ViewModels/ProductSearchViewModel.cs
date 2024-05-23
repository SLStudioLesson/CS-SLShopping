using SLShopping.Models;
namespace SLShopping.ViewModels;

public class ProductSearchViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }

    public string? Image { get; set; }
    public float Length { get; set; }
    public float Height { get; set; }
    public float Width { get; set; }
    public float Weight { get; set; }

    public List<Product> Results { get; set; }
}
