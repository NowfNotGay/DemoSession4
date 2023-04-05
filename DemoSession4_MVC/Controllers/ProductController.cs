using DemoSession4_MVC.Helpers;
using DemoSession4_MVC.Models;
using DemoSession4_MVC.Models.Interface;
using DemoSession4_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System.Diagnostics;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Net.Mime.MediaTypeNames;

namespace DemoSession4_MVC.Controllers;
[Route("Product")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _webHostEnvironment = webHostEnvironment;
    }

    [Route("Index")]
    [Route("")]
    public IActionResult Index()
    {
        return View(new ProductIndexViewModels()
        {
            Categories = _categoryService.GetCategory(),
            Products = _productService.GetProducts()
        });
    }

    [Route("Index2")]
    public IActionResult Index2()
    {
        return View(new ProductIndexViewModels()
        {
            Categories = _categoryService.GetCategory(),
            Products = _productService.GetProducts()
        });
    }
    [Route("Index3")]
    [Route("~/")]
    public IActionResult Index3(int page= 1,int pageSize = 2)
    {
        PagedList<Product> pagedList = new PagedList<Product>(_productService.GetProducts().AsQueryable(),page,pageSize);
        return View(new ProductIndexViewModels()
        {
            Categories = _categoryService.GetCategory(),
            Products = _productService.GetProducts(),
            Products_page = pagedList
        });
    }

    [Route("details")]
    public IActionResult Details(int id)
    {
        return View(_productService.GetProductById(id));
    }

    [Route("searchbykeyword")]
    public IActionResult Searchbykeyword(string keyword)
    {
        ViewBag.keyword = keyword;
        return View("index", new ProductIndexViewModels()
        {
            Categories = _categoryService.GetCategory(),
            Products = _productService.GetProductByKeyWord(keyword)
        });
    }


    [Route("searchbyprice")]
    public IActionResult Searchbyprice(double max, double min)
    {
        ViewBag.max = max;
        ViewBag.min = min;
        return View("index", new ProductIndexViewModels()
        {
            Categories = _categoryService.GetCategory(),
            Products = _productService.GetProductByPrice(max, min)
        });
    }

    [Route("searchByCategory")]
    public IActionResult SearchByCategory(int categoryId)
    {
        ViewBag.cateid = categoryId;
        if(categoryId == -1)
            return View("index", new ProductIndexViewModels()
            {
                Categories = _categoryService.GetCategory(),
                Products = _productService.GetProducts()
            });
        
        return View("index", new ProductIndexViewModels()
        {
            Categories = _categoryService.GetCategory(),
            Products = _productService.GetProductByCategoryId(categoryId)
        });
    }
    [HttpGet]
    [Route("add")]
    public IActionResult Add()
    {
        ViewBag.Categories = _categoryService.GetCategory();
        var product = new Product()
        {
            Created = DateTime.Now
        };
        return View(product);
    }

    [HttpPost]
    [Route("add")]
    public IActionResult Add(Product product, IFormFile photo)
    {
        if (photo is not null)
        {
            product.Photo = FileHelper.genderateFileName(photo.FileName);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", product.Photo);//Tao ra duong dan
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
        }
        else
        {
            product.Photo = "no-image.jpg";
        }
        if (_productService.AddProduct(product))
        {
            TempData["msg"] = "succesfull";
        }
        else
        {
            TempData["msg"] = "Fail";
        };
        return RedirectToAction("Index");
    }

    [Route("delete")]
    public IActionResult Delete(int id)
    {
        TempData["msg"] = (_productService.DeleteProduct(id))? "Success": "Fail";
        return RedirectToAction("Index");
    }

    [Route("edit")]
    [HttpGet]
    public IActionResult Edit(int id)
    {
        ViewBag.Categories = _categoryService.GetCategory();
        return View(_productService.GetProductById(id));
    }


    [Route("edit")]
    [HttpPost]
    public IActionResult Edit(int id,Product product, IFormFile photo)
    {
        if (photo == null)
        {
            product.Photo = _productService.GetProductByIdNoTracking(id).Photo;
        }
        else
        {
            product.Photo = FileHelper.genderateFileName(photo.FileName);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", product.Photo);//Tao ra duong dan
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
        }
        Debug.WriteLine("value:" ,product.CategoryId);
        TempData["msg"] = (_productService.EditProduct(product)) ? "Done" : "Fail";
        return RedirectToAction("Index");
    }

    [Route("searchbykeywordselect")]
    public IActionResult SearchbykeywordAjax(string keyword)
    {
        if (string.IsNullOrEmpty(keyword) || string.IsNullOrWhiteSpace(keyword))
        {
            return new JsonResult(_productService.GetProductsAjax());

        }
        return new JsonResult(_productService.GetProductByKeyWordSelect(keyword));
    }

    [Route("searchbycategoryid")]
    public IActionResult SearchBycategoryid(int id)
    {
        if (id<0)
        {
            return new JsonResult(_productService.GetProductsAjax());

        }
        return new JsonResult(_productService.GetProductsByCategoryIdAjax(id));
    }

    [Route("findbykeywordAutoComplete")]
    public IActionResult FindBykeywordAutoComplete(string term)
    {
        return new JsonResult(_productService.GetProductsByKeyword( term));
    }

    [Route("getProductById")]
    public IActionResult FindBykeywordAutoComplete(int id)
    {
        return new JsonResult(_productService.GetProductsByIdAjax(id));
    }


    [Route("searchbykeywordPagination")]
    public IActionResult SearchByKeywordPagination(string keyword, int page=1 ,int pageSize = 2)
    {
        ViewBag.keyword = keyword;
        return View("index3", new ProductIndexViewModels()
        {
            Categories = _categoryService.GetCategory(),
            Products = _productService.GetProductByKeyWord(keyword),
            Products_page = new PagedList<Product>(_productService.GetProductByKeyWord(keyword).AsQueryable(),page,pageSize)
        });
    }
}
