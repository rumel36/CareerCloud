#pragma checksum "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\CompanyJobPoco\PostedJob.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5d8b83eb9b2281a06aa111fa9dd20028bc8a1fe1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CompanyJobPoco_PostedJob), @"mvc.1.0.view", @"/Views/CompanyJobPoco/PostedJob.cshtml")]
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
#line 1 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\_ViewImports.cshtml"
using CareerCloud.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\_ViewImports.cshtml"
using CareerCloud.MVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5d8b83eb9b2281a06aa111fa9dd20028bc8a1fe1", @"/Views/CompanyJobPoco/PostedJob.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5042336d488fc6020521578f212d765d80eacae5", @"/Views/_ViewImports.cshtml")]
    public class Views_CompanyJobPoco_PostedJob : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CareerCloud.MVC.ViewModels.JobPostedVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h4>\r\n</h4>\r\n");
#nullable restore
#line 5 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\CompanyJobPoco\PostedJob.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";
    Guid companyId = (Guid)TempData["CompanyId"];

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h2><b>Company Name:&nbsp</b>  ");
#nullable restore
#line 8 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\CompanyJobPoco\PostedJob.cshtml"
                              Write(ViewBag.CompanyName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n");
            WriteLiteral("<div>\r\n    <table class=\"table\">\r\n        <tr >\r\n            <td>");
#nullable restore
#line 14 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\CompanyJobPoco\PostedJob.cshtml"
           Write(Html.ActionLink("+Create New Job", "Create", new { id = companyId }));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        </tr>\r\n    </table>\r\n</div>\r\n<p>\r\n\r\n</p>\r\n\r\n");
#nullable restore
#line 22 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\CompanyJobPoco\PostedJob.cshtml"
 if (!Model.Any())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>No Jop Posted</p>\r\n");
#nullable restore
#line 25 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\CompanyJobPoco\PostedJob.cshtml"
}
else
{

    

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\CompanyJobPoco\PostedJob.cshtml"
Write(Html.Partial("_Job"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\CompanyJobPoco\PostedJob.cshtml"
                         ;
}

#line default
#line hidden
#nullable disable
            WriteLiteral("<p >\r\n\r\n    ");
#nullable restore
#line 33 "G:\Humber\NET.22-4-Omar-Sharif-N01408020\NET.22-4-Omar-Sharif-N01408020\CareerCloud.MVC\Views\CompanyJobPoco\PostedJob.cshtml"
Write(Html.ActionLink("Back To List", "Index", new { Controller = "CompanyProfiles" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n</p>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CareerCloud.MVC.ViewModels.JobPostedVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
