#pragma checksum "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3338263a9248a02754bd52b32edff55d5818deda"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MyPurchases_Index), @"mvc.1.0.view", @"/Views/MyPurchases/Index.cshtml")]
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
#line 1 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\_ViewImports.cshtml"
using team5_SC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\_ViewImports.cshtml"
using team5_SC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3338263a9248a02754bd52b32edff55d5818deda", @"/Views/MyPurchases/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ce34657317260d4fe0e8be08e8225dbef4457c38", @"/Views/_ViewImports.cshtml")]
    public class Views_MyPurchases_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
  
    ViewData["Title"] = "My Purchases";
    List<MyPurchase> myPurchases = (List<MyPurchase>)ViewData["myPurchases"];


#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 37 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
 using (Html.BeginForm())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>\r\n        Find By Name: ");
#nullable restore
#line 40 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
                 Write(Html.TextBox("searchStr"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <input type=\"submit\" value=\"Search\" />\r\n    </p>\r\n");
#nullable restore
#line 43 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table class=\"table\">\r\n    <tr>\r\n        <th>\r\n            ");
#nullable restore
#line 48 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
       Write(Html.ActionLink("PurchaseDate", "Index", new { sortOrder = ViewBag.sortbyPurchaseDate }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th>\r\n            ");
#nullable restore
#line 51 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
       Write(Html.ActionLink("Id", "Index", new { sortOrder = ViewBag.sortbyId }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th>\r\n            Quantity\r\n        </th>\r\n        <th>ActivationCodeId</th>\r\n    </tr>\r\n");
#nullable restore
#line 58 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
     foreach (team5_SC.Models.MyPurchase myPurchase in myPurchases)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 62 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
           Write(myPurchase.PurchaseDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 65 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
           Write(myPurchase.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 68 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
           Write(myPurchase.Qty);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 73 "C:\Users\Leon (School)\source\repos\lyeow-SA54\team5-ca-sc\team5_SC\Views\MyPurchases\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
