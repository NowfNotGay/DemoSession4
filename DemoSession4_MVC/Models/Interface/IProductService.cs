namespace DemoSession4_MVC.Models.Interface;

public interface IProductService
{
    public List<Product> GetProducts();
    public Product GetProductById(int id);
    public Product GetProductByIdNoTracking(int id);

    public List<Product> GetProductByKeyWord(string keyword);
    public List<Product> GetProductByPrice(double max, double min);
    public List<Product> GetProductByCategoryId(int id);

    public bool AddProduct(Product product);
    public bool DeleteProduct(int id);
    public bool EditProduct(Product product);

    public List<Product> GetProductsDes(int n);
}
