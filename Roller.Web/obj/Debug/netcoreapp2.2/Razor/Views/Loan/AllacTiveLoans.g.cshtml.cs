#pragma checksum "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3574dfcb743c2966b94b1c4c6feb10668bfa52c3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Loan_AllacTiveLoans), @"mvc.1.0.view", @"/Views/Loan/AllacTiveLoans.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Loan/AllacTiveLoans.cshtml", typeof(AspNetCore.Views_Loan_AllacTiveLoans))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3574dfcb743c2966b94b1c4c6feb10668bfa52c3", @"/Views/Loan/AllacTiveLoans.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b76fe4aa689107ee742bd2c87b4739dd86a4ce9b", @"/Views/_ViewImports.cshtml")]
    public class Views_Loan_AllacTiveLoans : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Roller.Repository.LoanRepos.LoanModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
  
   // ViewData["Title"] = "AllacTiveLoans";

#line default
#line hidden
            BeginContext(111, 627, true);
            WriteLiteral(@"<br />
<br />
<br />
<br />
<br />
<br />
<h2>All Loan Accounts</h2>

<table class=""table"">
    <tr>
        <th>
            Account Number:
        </th>
        <th>
            loan officier
        </th>
        <th>
            Loan Amount
        </th>
        <th>
            Interest Rate
        </th>
        <th>
            Amount Paid
        </th>
        <th>
            Sum Total
        </th>
        <th>
            Loan Date
        </th>
        <th>
            Duration
        </th>
        <th>
            Deadline
        </th>
        <th></th>
    </tr>

");
            EndContext();
#line 45 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
            BeginContext(779, 36, true);
            WriteLiteral("    <tr>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(816, 48, false);
#line 49 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
       Write(Html.DisplayFor(modelItem => item.AccountNumber));

#line default
#line hidden
            EndContext();
            BeginContext(864, 43, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(908, 47, false);
#line 52 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
       Write(Html.DisplayFor(modelItem => item.LoanOfficier));

#line default
#line hidden
            EndContext();
            BeginContext(955, 43, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(999, 46, false);
#line 55 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
       Write(Html.DisplayFor(modelItem => item.Loan_Amount));

#line default
#line hidden
            EndContext();
            BeginContext(1045, 43, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(1089, 48, false);
#line 58 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
       Write(Html.DisplayFor(modelItem => item.Interest_Rate));

#line default
#line hidden
            EndContext();
            BeginContext(1137, 43, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(1181, 51, false);
#line 61 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
       Write(Html.DisplayFor(modelItem => item.Loan_Amount_Paid));

#line default
#line hidden
            EndContext();
            BeginContext(1232, 43, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(1276, 47, false);
#line 64 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
       Write(Html.DisplayFor(modelItem => item.AmountTo_Pay));

#line default
#line hidden
            EndContext();
            BeginContext(1323, 43, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(1367, 44, false);
#line 67 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
       Write(Html.DisplayFor(modelItem => item.Loan_Date));

#line default
#line hidden
            EndContext();
            BeginContext(1411, 43, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(1455, 43, false);
#line 70 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
       Write(Html.DisplayFor(modelItem => item.Duration));

#line default
#line hidden
            EndContext();
            BeginContext(1498, 43, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(1542, 43, false);
#line 73 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
       Write(Html.DisplayFor(modelItem => item.Deadline));

#line default
#line hidden
            EndContext();
            BeginContext(1585, 28, true);
            WriteLiteral("\r\n        </td>\r\n    </tr>\r\n");
            EndContext();
#line 76 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Loan\AllacTiveLoans.cshtml"
    }

#line default
#line hidden
            BeginContext(1620, 12, true);
            WriteLiteral("\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Roller.Repository.LoanRepos.LoanModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
