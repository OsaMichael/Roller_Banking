#pragma checksum "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Wallet\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2d43feb1d226c7aed0d2cfc985e8f651deecd267"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Wallet_Index), @"mvc.1.0.view", @"/Views/Wallet/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Wallet/Index.cshtml", typeof(AspNetCore.Views_Wallet_Index))]
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
#line 1 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\_ViewImports.cshtml"
using Roller.Web;

#line default
#line hidden
#line 2 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\_ViewImports.cshtml"
using Roller.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2d43feb1d226c7aed0d2cfc985e8f651deecd267", @"/Views/Wallet/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b76fe4aa689107ee742bd2c87b4739dd86a4ce9b", @"/Views/_ViewImports.cshtml")]
    public class Views_Wallet_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Roller.Repository.Customers.WalletDto>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Wallet\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(87, 190, true);
            WriteLiteral("\r\n<br />\r\n<br />\r\n<br />\r\n<br />\r\n<br />\r\n\r\n\r\n<div style=\"text-align:center\">\r\n    <small style=\"text-align:center; color:cadetblue\">Wallet Balance</small>\r\n    <h4 style=\"color:darkcyan\"># ");
            EndContext();
            BeginContext(278, 20, false);
#line 15 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Wallet\Index.cshtml"
                            Write(Model.AccountBalance);

#line default
#line hidden
            EndContext();
            BeginContext(298, 15, true);
            WriteLiteral("</h4>\r\n</div>\r\n");
            EndContext();
            BeginContext(347, 154, true);
            WriteLiteral("<table class=\"table table-striped\">\r\n    <thead>\r\n        <tr>\r\n            <th style=\"text-align:center\">\r\n                <a class=\"btn btn-info mg-r-5\"");
            EndContext();
            BeginWriteAttribute("href", "\r\n                   href=\"", 501, "\"", 589, 1);
#line 23 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Wallet\Index.cshtml"
WriteAttributeValue("", 528, Url.Action("AddWallet", "Wallet", new {Model.AccountNumber}), 528, 61, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(590, 93, true);
            WriteLiteral("><span class=\"fa fa-pencil\"></span><i class=\"pe-7s-edit btn-icon-wrapper\"></i>Add Money</a>\r\n");
            EndContext();
            BeginContext(869, 48, true);
            WriteLiteral("            </th>\r\n        </tr>\r\n    </thead>\r\n");
            EndContext();
            BeginContext(1503, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1523, 12, true);
            WriteLiteral("\r\n</table>\r\n");
            EndContext();
            BeginContext(1547, 2, true);
            WriteLiteral("\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Roller.Repository.Customers.WalletDto> Html { get; private set; }
    }
}
#pragma warning restore 1591
