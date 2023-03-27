using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DemoSession4_MVC.TagHelpers;
// You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
[HtmlTargetElement("addressinfo",TagStructure = TagStructure.WithoutEndTag | TagStructure.NormalOrSelfClosing)]
public class AddressInfoTagHepler : TagHelper
{
    [HtmlAttributeName("street")]
    public string? Street { get; set; }
    [HtmlAttributeName("ward")]
    public string? Ward { get; set; }
    [HtmlAttributeName("district")]
    public string? District { get; set; }


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "";
        string s = "";
        s += "<h3>Địa chỉ</h3>" + "" +
            "<br/>Street: " + Street +
            "<br/> Ward: " + Ward +
            "<br/> Dictrict: " + District;
        output.Content.SetHtmlContent(s);
    }
}
