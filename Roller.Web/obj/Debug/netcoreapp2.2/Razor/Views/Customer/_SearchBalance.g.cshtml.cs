#pragma checksum "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Customer\_SearchBalance.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "289a1c7234ddf5d8f34eb2af8c1117cf65aa72a3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer__SearchBalance), @"mvc.1.0.view", @"/Views/Customer/_SearchBalance.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Customer/_SearchBalance.cshtml", typeof(AspNetCore.Views_Customer__SearchBalance))]
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
#line 2 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Customer\_SearchBalance.cshtml"
using Roller.Repository.Extensions;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"289a1c7234ddf5d8f34eb2af8c1117cf65aa72a3", @"/Views/Customer/_SearchBalance.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b76fe4aa689107ee742bd2c87b4739dd86a4ce9b", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer__SearchBalance : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Roller.Repository.Customers.CustomerDto>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(60, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(160, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 5 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Customer\_SearchBalance.cshtml"
  
    ViewData["Title"] = "Search";

#line default
#line hidden
            BeginContext(204, 48, true);
            WriteLiteral("<br />\r\n<br />\r\n<br />\r\n<br />\r\n<br />\r\n<br />\r\n");
            EndContext();
            BeginContext(766, 366, true);
            WriteLiteral(@"<div class=""container pt-5"">
    <div class=""table-responsive"">
        <table class=""table table-striped table-sm w-75 offset-2"">
            <thead class=""text-center"">
                <tr>
                    <th>Account number</th>
                    <th>Balance</th>
                </tr>
            </thead>
            <tbody class=""text-center"">
");
            EndContext();
#line 38 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Customer\_SearchBalance.cshtml"
                 foreach (var acc in Model)
                {

#line default
#line hidden
            BeginContext(1196, 54, true);
            WriteLiteral("                    <tr>\r\n                        <td>");
            EndContext();
            BeginContext(1251, 17, false);
#line 41 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Customer\_SearchBalance.cshtml"
                       Write(acc.AccountNumber);

#line default
#line hidden
            EndContext();
            BeginContext(1268, 35, true);
            WriteLiteral("</td>\r\n                        <td>");
            EndContext();
            BeginContext(1304, 28, false);
#line 42 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Customer\_SearchBalance.cshtml"
                       Write(acc.Balance.ToSwedishKrona());

#line default
#line hidden
            EndContext();
            BeginContext(1332, 34, true);
            WriteLiteral("</td>\r\n                    </tr>\r\n");
            EndContext();
#line 44 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Customer\_SearchBalance.cshtml"
                }

#line default
#line hidden
            BeginContext(1385, 22, true);
            WriteLiteral("            </tbody>\r\n");
            EndContext();
            BeginContext(1634, 42, true);
            WriteLiteral("        </table>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
            EndContext();
            BeginContext(1683, 2, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Roller.Repository.Customers.CustomerDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591
