using DemoSession4_MVC.Models.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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

    public dynamic GetProductByIdSelect(int id) => _databaseContext.Products.Where(p => p.Id == id).Select(p=> new
    {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price
    }).FirstOrDefault()!;

    public List<Product> GetProductByKeyWord(string keyword) => _databaseContext.Products.Where(p=>p.Name.Contains(keyword)).ToList();

    public dynamic GetProductByKeyWordSelect(string keyword)=>_databaseContext.Products.Where(p=>p.Name.Contains(keyword)).Select(p=>
        new
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quatity = p.Quantity,
            Description = p.Description,
            Status = p.Status,
            Photo = p.Photo,
            Created = p.Created,
            CategoryName = p.Category.Name
        }
    ).ToList();


    public List<Product> GetProductByPrice(double max, double min)=>_databaseContext.Products.Where(p=>p.Price <= max && p.Price >= min).ToList();

    public List<Product> GetProducts() => _databaseContext.Products.ToList();

    public dynamic GetProductsAjax()=> _databaseContext.Products.Select(p =>
        new
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quatity = p.Quantity,
            Description = p.Description,
            Status = p.Status,
            Photo = p.Photo,
            Created = p.Created,
            CategoryName = p.Category.Name
        }
    ).ToList();

    public dynamic GetProductsByCategoryIdAjax(int id)=>_databaseContext.Products.Where(p => p.CategoryId == id).Select(p=>
        new
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quatity = p.Quantity,
            Description = p.Description,
            Status = p.Status,
            Photo = p.Photo,
            Created = p.Created,
            CategoryName = p.Category.Name
}
    ).ToList();

    public dynamic GetProductsByIdAjax(int id) => _databaseContext.Products.Where(p => p.Id == id).Select(p =>
        new
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quatity = p.Quantity,
            Description = p.Description,
            Status = p.Status,
            Photo = p.Photo,
            Created = p.Created,
            CategoryName = p.Category.Name
        }
    ).FirstOrDefault()!;

    public List<string> GetProductsByKeyword(string keyword)=> _databaseContext.Products.Where(p => p.Name.Contains(keyword)).Select(p =>p.Name).ToList();

    public List<Product> GetProductsDes(int n)=>_databaseContext.Products.OrderByDescending(p => p.Id).Take(n).ToList();

    public dynamic GetProductsSelect()=> _databaseContext.Products.Select(p => new
    {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price
    }).ToList();
}
