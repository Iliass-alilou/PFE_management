#pragma checksum "C:\Users\Main\Desktop\.Net projet\PFE_Management\Views\Instructors\MesStages.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "81b06746077f2eef4b422672af4cfe5d2f2027cf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Instructors_MesStages), @"mvc.1.0.view", @"/Views/Instructors/MesStages.cshtml")]
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
#line 1 "C:\Users\Main\Desktop\.Net projet\PFE_Management\Views\_ViewImports.cshtml"
using PFE_Management;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Main\Desktop\.Net projet\PFE_Management\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Main\Desktop\.Net projet\PFE_Management\Views\_ViewImports.cshtml"
using PFE_Management.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"81b06746077f2eef4b422672af4cfe5d2f2027cf", @"/Views/Instructors/MesStages.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bef18aeb5957dbc7fe2d6892d3f36ec82d26054d", @"/Views/_ViewImports.cshtml")]
    public class Views_Instructors_MesStages : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<PFE_Management.Models.Stage>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Main\Desktop\.Net projet\PFE_Management\Views\Instructors\MesStages.cshtml"
  
    ViewData["Title"] = "All stages";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1> Liste des stages de cette Année </h1>\r\n\r\n\r\n");
            WriteLiteral("\r\n<p>");
#nullable restore
#line 66 "C:\Users\Main\Desktop\.Net projet\PFE_Management\Views\Instructors\MesStages.cshtml"
Write(ViewBag.idUser);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p> test </p>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<PFE_Management.Models.Stage>> Html { get; private set; }
    }
}
#pragma warning restore 1591
