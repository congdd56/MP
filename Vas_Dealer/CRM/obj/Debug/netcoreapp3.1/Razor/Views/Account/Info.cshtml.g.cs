#pragma checksum "F:\Project\Vas_Dealer\CRM\Views\Account\Info.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17804e0f3b5850b0ab46571683e84b7d925bc87e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Info), @"mvc.1.0.view", @"/Views/Account/Info.cshtml")]
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
#line 1 "F:\Project\Vas_Dealer\CRM\Views\_ViewImports.cshtml"
using VAS.Dealer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\Project\Vas_Dealer\CRM\Views\_ViewImports.cshtml"
using VAS.Dealer.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"17804e0f3b5850b0ab46571683e84b7d925bc87e", @"/Views/Account/Info.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"782901c819350ccc8176209e6f82a108c224e868", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Info : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<VAS.Dealer.Models.CC.AccountModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/modules/manager.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "F:\Project\Vas_Dealer\CRM\Views\Account\Info.cshtml"
  
    ViewData["Title"] = "Thông tin tài khoản";
    ViewData["SubTitle"] = "Chỉnh sửa thông tin, đổi mật khẩu..";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "17804e0f3b5850b0ab46571683e84b7d925bc87e3695", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-md-6 pr-0"">
        <div class=""box box-success"">
            <div class=""box-header with-border"">
                <h3 class=""box-title""><i class=""fa fa-info-circle text-success""></i>&nbsp;Thông tin chung</h3>
            </div>
            <div class=""box-body"">
                <div class=""row"">
                    <div class=""col-md-12"">
                        <div class=""form-group"">
                            ");
#nullable restore
#line 19 "F:\Project\Vas_Dealer\CRM\Views\Account\Info.cshtml"
                       Write(Html.Hidden("Id", Model.Id, new { @class = "form-control rounded" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            <label for=\"UserName\">Tài khoản</label>\r\n                            ");
#nullable restore
#line 21 "F:\Project\Vas_Dealer\CRM\Views\Account\Info.cshtml"
                       Write(Html.TextBox("UserName", Model.UserName, new { @class = "form-control rounded", @required = "", @placeholder = "Tài khoản", @readonly = "true" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-md-12\">\r\n                        <div class=\"form-group\">\r\n                            <label for=\"FullName\">Họ và tên</label>\r\n                            ");
#nullable restore
#line 27 "F:\Project\Vas_Dealer\CRM\Views\Account\Info.cshtml"
                       Write(Html.TextBox("FullName", Model.FullName, new { @class = "form-control rounded", @required = "", @placeholder = "Họ và tên" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-md-12\">\r\n                        <div class=\"form-group\">\r\n                            <label for=\"Email\">Số điện thoại</label>\r\n                            ");
#nullable restore
#line 33 "F:\Project\Vas_Dealer\CRM\Views\Account\Info.cshtml"
                       Write(Html.TextBox("PhoneNumber", Model.PhoneNumber, new
                            {
                                @class = "form-control rounded",
                                @required = "",
                                @placeholder = "Số điện thoại"
                            }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-md-12\">\r\n                        <div class=\"form-group\">\r\n                            <label for=\"Email\">Email</label>\r\n                            ");
#nullable restore
#line 44 "F:\Project\Vas_Dealer\CRM\Views\Account\Info.cshtml"
                       Write(Html.TextBox("Email", Model.Email, new { @class = "form-control rounded", @required = "", @placeholder = "Email" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </div>
                    </div>
                    <div class=""col-md-12"">
                        <div class=""form-group"">
                            <button type=""button"" id=""btn-updateInfo"" class=""btn btn-flat bg-green-active btn-sm btn-updateInfo"">
                                <i class=""fa fa-edit text-white""></i>&nbsp;Cập nhật
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-md-6"">
        <div class=""box box-danger"">
            <div class=""box-header with-border"">
                <h3 class=""box-title""><i class=""fa fa-unlock-alt text-red""></i>&nbsp;Đổi mật khẩu</h3>
            </div>
            <div class=""box-body"">
                <div class=""row"">
                    <div class=""col-md-12"">
                        <div class=""form-group"">
                            <label for=""password"">Mật khẩu hiện tại</label");
            WriteLiteral(@">
                            <input type=""password"" class=""form-control"" id=""password"" placeholder=""Mật khẩu hiện tại"">
                        </div>
                    </div>
                    <div class=""col-md-12"">
                        <div class=""form-group"">
                            <label for=""passwordNew"">Mật khẩu mới</label>
                            <input type=""password"" class=""form-control"" id=""passwordNew"" placeholder=""Mật khẩu mới"">
                        </div>
                    </div>
                    <div class=""col-md-12"">
                        <div class=""form-group"">
                            <label for=""passwordConfirm,"">Xác nhận lại mật khẩu</label>
                            <input type=""password"" class=""form-control"" id=""passwordConfirm"" placeholder=""Nhập lại mật khẩu"">
                        </div>
                    </div>
                    <div class=""col-md-12"">
                        <button type=""button"" id=""btn-changePassword"" class=""");
            WriteLiteral(@"btn btn-flat bg-green-active btn-sm"">
                            <i class=""fa fa-unlock-alt text-white""></i>&nbsp;Đổi mật khẩu
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<VAS.Dealer.Models.CC.AccountModel> Html { get; private set; }
    }
}
#pragma warning restore 1591