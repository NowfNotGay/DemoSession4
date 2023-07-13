using DemoSession4_MVC.Models;
using DemoSession4_MVC.Models.Interface;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DemoSession4_MVC.TagHelpers;
// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
[HtmlTargetElement("latestProduct",TagStructure = TagStructure.WithoutEndTag | TagStructure.NormalOrSelfClosing)]
public class LastestProductTagHelper : TagHelper
{
    private readonly IProductService _productService;
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext viewContext { get; set; }
    private readonly IHtmlHelper _htmlHelper;
    public LastestProductTagHelper(IProductService productService, IHtmlHelper htmlHelper)
    {
        _productService = productService;
        _htmlHelper = htmlHelper;
    }

    [HtmlAttributeName("n")]
    public int N { get; set; }
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        (_htmlHelper as IViewContextAware).Contextualize(viewContext);
        _htmlHelper.ViewBag.id = 123;
        _htmlHelper.ViewBag.username = "acc1";
        output.TagName = "";
        output.Content.SetHtmlContent(await _htmlHelper.PartialAsync("TagHelpers/LatestProduct/Index",_productService.GetProductsDes(N)));
    }
}
