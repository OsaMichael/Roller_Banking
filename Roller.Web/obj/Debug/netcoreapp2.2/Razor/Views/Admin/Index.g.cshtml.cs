#pragma checksum "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f875bf04851913cb32ee9e029e53b508bcfe50de"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Index), @"mvc.1.0.view", @"/Views/Admin/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Index.cshtml", typeof(AspNetCore.Views_Admin_Index))]
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
#line 3 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
using Roller.Web.Models.UserViewModels;

#line default
#line hidden
#line 4 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
using Roller.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f875bf04851913cb32ee9e029e53b508bcfe50de", @"/Views/Admin/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b76fe4aa689107ee742bd2c87b4739dd86a4ce9b", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Roller.Repository.AccountRepo.UserModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ClaimForm", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_DeleteForm", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_UserEditForm", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 6 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(160, 37, true);
            WriteLiteral("\r\n<div class=\"container pt-3 pb-3\">\r\n");
            EndContext();
            BeginContext(415, 304, true);
            WriteLiteral(@"    <h1 class=""text-center pb-5"">Manage bank users</h1>
    <table class=""table table-sm w-75 offset-2 text-center"">
        <thead>
            <tr>
                <th>Email</th>
                <th>Role</th>
                <th>Manage</th>
            </tr>
        </thead>
        <tbody>
");
            EndContext();
#line 29 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
             foreach (var user in Model.Users)
            {

#line default
#line hidden
            BeginContext(782, 64, true);
            WriteLiteral("                <tr class=\"text-left\">\r\n                    <td>");
            EndContext();
            BeginContext(847, 10, false);
#line 32 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                   Write(user.Email);

#line default
#line hidden
            EndContext();
            BeginContext(857, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 34 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                     foreach (var claim in user.Claims)
                    {

#line default
#line hidden
            BeginContext(991, 28, true);
            WriteLiteral("                        <td>");
            EndContext();
            BeginContext(1020, 10, false);
#line 36 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                       Write(claim.Type);

#line default
#line hidden
            EndContext();
            BeginContext(1030, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 37 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                    }

#line default
#line hidden
            BeginContext(1060, 26, true);
            WriteLiteral("                    <td>\r\n");
            EndContext();
#line 39 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                         if (User.Identity.Name != user.Email && User.HasClaim(Claims.Admin, "true"))
                        {

#line default
#line hidden
            BeginContext(1216, 129, true);
            WriteLiteral("                            <button type=\"button\" class=\"btn btn-sm btn-outline-primary\" data-toggle=\"modal\" data-target=\"#claim-");
            EndContext();
            BeginContext(1346, 7, false);
#line 41 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                                                                                                                            Write(user.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1353, 248, true);
            WriteLiteral("\">\r\n                                <i class=\"fas fa-user-tag\"></i> Change Role\r\n                            </button>\r\n                            <button type=\"button\" class=\"btn btn-sm btn-outline-primary\" data-toggle=\"modal\" data-target=\"#edit-");
            EndContext();
            BeginContext(1602, 7, false);
#line 44 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                                                                                                                           Write(user.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1609, 244, true);
            WriteLiteral("\">\r\n                                <i class=\"fas fa-user-edit\"></i> Edit\r\n                            </button>\r\n                            <button type=\"button\" class=\"btn btn-sm btn-outline-primary\" data-toggle=\"modal\" data-target=\"#delete-");
            EndContext();
            BeginContext(1854, 7, false);
#line 47 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                                                                                                                             Write(user.Id);

#line default
#line hidden
            EndContext();
            BeginContext(1861, 117, true);
            WriteLiteral("\">\r\n                                <i class=\"fas fa-user-minus\"></i> Delete\r\n                            </button>\r\n");
            EndContext();
#line 50 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(2062, 128, true);
            WriteLiteral("                            <button type=\"button\" class=\"btn btn-sm btn-outline-primary\" data-toggle=\"modal\" data-target=\"#edit-");
            EndContext();
            BeginContext(2191, 7, false);
#line 53 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                                                                                                                           Write(user.Id);

#line default
#line hidden
            EndContext();
            BeginContext(2198, 114, true);
            WriteLiteral("\">\r\n                                <i class=\"fas fa-user-edit\"></i> Edit\r\n                            </button>\r\n");
            EndContext();
#line 56 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
                        }

#line default
#line hidden
            BeginContext(2339, 47, true);
            WriteLiteral("                    </td>\r\n                    ");
            EndContext();
            BeginContext(2386, 43, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f875bf04851913cb32ee9e029e53b508bcfe50de10252", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 58 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = user;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2429, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(2451, 44, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f875bf04851913cb32ee9e029e53b508bcfe50de11934", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#line 59 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = user;

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2495, 22, true);
            WriteLiteral("\r\n                    ");
            EndContext();
            BeginContext(2517, 99, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f875bf04851913cb32ee9e029e53b508bcfe50de13616", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#line 60 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = (new EditUserModel{Email = user.Email, UserId = user.Id});

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2616, 25, true);
            WriteLiteral("\r\n                </tr>\r\n");
            EndContext();
#line 62 "C:\Users\CI\Music\Roller (2)\Roller\Roller.Web\Views\Admin\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(2656, 38, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Roller.Repository.AccountRepo.UserModel> Html { get; private set; }
    }
}
#pragma warning restore 1591