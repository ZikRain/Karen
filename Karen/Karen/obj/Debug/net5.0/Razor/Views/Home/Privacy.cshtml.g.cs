#pragma checksum "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "db73b006a0e550095366fd7cb2522db7cfb3a484"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Privacy), @"mvc.1.0.view", @"/Views/Home/Privacy.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\_ViewImports.cshtml"
using Karen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\_ViewImports.cshtml"
using Karen.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
using Karen.Models.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db73b006a0e550095366fd7cb2522db7cfb3a484", @"/Views/Home/Privacy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"619755094bfba5f9366761992a03fff1e20fdba9", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Privacy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Product>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/site.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
  
    ViewData["Title"] = "Privacy Policy";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"container content\">\r\n     <div class=\"row\">\r\n          <div class=\"col-md-4\">Категория\r\n            <div class=\"list-group\">\r\n            ");
#nullable restore
#line 12 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
       Write(Html.ActionLink("Вcе","Privacy","Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 13 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
       Write(Html.ActionLink("Диваны","Privacy","Home", new {type=1}));

#line default
#line hidden
#nullable disable
            WriteLiteral(" \r\n            ");
#nullable restore
#line 14 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
       Write(Html.ActionLink("Кровать","Privacy","Home", new {type=2}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 15 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
       Write(Html.ActionLink("Шкаф","Privacy","Home", new {type=3}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 16 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
       Write(Html.ActionLink("Стол","Privacy","Home", new {type=4}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n          </div>\r\n  \r\n");
#nullable restore
#line 20 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
              
                
                 foreach (Product item in Model)
                  {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                  <div class=""col-md-8"">
                    <div class=""row"">
                        <div class=""col-sm-4"">
                            <div class=""product"">
                            <div class=""product-img"">
                            <a href=""#""><img");
            BeginWriteAttribute("src", " src=\"", 1006, "\"", 1031, 1);
#nullable restore
#line 29 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
WriteAttributeValue("", 1012, item.product_image, 1012, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 1032, "\"", 1038, 0);
            EndWriteAttribute();
            WriteLiteral("></a>\r\n                            </div>\r\n                            <p class=\"product-title\">\r\n                            <a href=\"#\">");
#nullable restore
#line 32 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
                                   Write(item.product_name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                            </p>\r\n                            <p class=\"product-price\">Price: €");
#nullable restore
#line 34 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
                                                        Write(item.product_price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </div>\r\n                        </div>\r\n                     </div>\r\n                   </div>\r\n");
#nullable restore
#line 39 "E:\Karen\KarenNew\Karen\Karen\Karen\Views\Home\Privacy.cshtml"
                   }   
                

#line default
#line hidden
#nullable disable
            WriteLiteral("            \r\n             \r\n     </div>\r\n </div>\r\n\r\n\r\n ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "db73b006a0e550095366fd7cb2522db7cfb3a4847851", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Product>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591