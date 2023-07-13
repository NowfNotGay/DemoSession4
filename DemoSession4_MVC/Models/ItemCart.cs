using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DemoSession4_MVC.Models;

public class ItemCart
{
    public ProductCart ProductCart { get; set; }
    public int Quantity { get; set; }
}
