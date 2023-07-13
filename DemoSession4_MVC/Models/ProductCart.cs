namespace DemoSession4_MVC.Models;

public class ProductCart
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Photo { get; set; }

    public ProductCart(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
        Photo = product.Photo;
    }

    public ProductCart()
    {
    }
}
