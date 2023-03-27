using DemoSession4_MVC.Models;
using DemoSession4_MVC.Models.Interface;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DemoSession4_MVC.TagHelpers;
// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
[HtmlTargetElement("category",TagStructure = TagStructure.WithoutEndTag | TagStructure.NormalOrSelfClosing)]
public class CategoryTagHelper : TagHelper
{
    private readonly ICategoryService _categoryService;

    public CategoryTagHelper(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "";
        string s = "<ul>";
        foreach (var item in _categoryService.GetCategory())
        {
            s+= "<li>"+item.Name+"</li>";
        }
        s += "</ul>";
        output.Content.SetHtmlContent(s);
    }
}
