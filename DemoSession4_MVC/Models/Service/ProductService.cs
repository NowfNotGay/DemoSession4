using DemoSession4_MVC.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace DemoSession4_MVC.Models.Service;

public class ProductService : IProductService
{
    private readonly DatabaseContext _databaseContext;

    public ProductService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool AddProduct(Product product)
    {
        if(product == null)
        {
            return false;
        }

        _databaseContext.Products.Add(product);
        return _databaseContext.SaveChanges() > 0;
    }

    public bool DeleteProduct(int id)
    {
        try
        {
            _databaseContext.Products.Remove(_databaseContext.Products.Find(id));
            return _databaseContext.SaveChanges() > 0;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public bool EditProduct(Product product)
    {
        try
        {
            _databaseContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public List<Product> GetProductByCategoryId(int id) => _databaseContext.Products.Where(p => p.CategoryId == id).ToList();

    public Product GetProductById(int id) => _databaseContext.Products.Find(id);

    public Product GetProductByIdNoTracking(int id) => _databaseContext.Products.AsNoTracking().SingleOrDefault(p => p.Id == id);

    public List<Product> GetProductByKeyWord(string keyword) => _databaseContext.Products.Where(p=>p.Name.Contains(keyword)).ToList();

    public List<Product> GetProductByPrice(double max, double min)=>_databaseContext.Products.Where(p=>p.Price <= max && p.Price >= min).ToList();

    public List<Product> GetProducts() => _databaseContext.Products.ToList();
    public List<Product> GetProductsDes(int n)=>_databaseContext.Products.OrderByDescending(p => p.Id).Take(n).ToList();
}
