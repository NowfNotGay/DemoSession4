using DemoSession4_MVC.Models.Interface;

namespace DemoSession4_MVC.Models.Service;

public class CategoryService : ICategoryService
{
    private readonly DatabaseContext _databaseContext;

    public CategoryService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    public dynamic GetCategory()=>_databaseContext.Categories;

    public dynamic GetCategoryByLevel()
    {
        throw new NotImplementedException();
    }

    private List<Category> dequy(List<Category> categories,int parent = 0,string level = "")
    {
        foreach (var category in categories)
        {
            category.Name = level + category.Name;
            if (category.parent = parent)
            {
                dequy(categories,parent = category.id,level = "--|")
            }
        }
        return categories;
    }
}
