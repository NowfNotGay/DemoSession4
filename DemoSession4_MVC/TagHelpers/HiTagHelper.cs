using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DemoSession4_MVC.TagHelpers;
// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
[HtmlTargetElement("hi",TagStructure = TagStructure.WithoutEndTag | TagStructure.NormalOrSelfClosing)]
public class HiTagHelper : TagHelper
{
    [HtmlAttributeName("hoten")]
    public string Fullname { get; set; }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "";
        output.Content.SetHtmlContent("Hi"+Fullname);
    }
}
