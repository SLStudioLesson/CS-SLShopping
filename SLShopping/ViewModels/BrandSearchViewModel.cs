using SLShopping.Models;

namespace SLShopping.ViewModels;

public class BrandSearchViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Brand> Results { get; set; }
}