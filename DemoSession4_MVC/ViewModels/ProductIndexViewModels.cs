using DemoSession4_MVC.Models;
using PagedList.Core;

namespace DemoSession4_MVC.ViewModels;

public class ProductIndexViewModels
{
    public List<Product> Products { get; set; }
    public List<Category> Categories { get; set; }
    public PagedList<Product> Products_page { get; set; }
}
