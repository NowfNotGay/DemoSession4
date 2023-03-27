using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DemoSession4_MVC.TagHelpers;
// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
[HtmlTargetElement("hello",TagStructure = TagStructure.WithoutEndTag | TagStructure.NormalOrSelfClosing)]
public class HelloTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "";
        output.Content.SetHtmlContent("<b><i><u>hello</u></i></b>");
    }
}
