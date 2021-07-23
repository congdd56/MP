#pragma checksum "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "40822e7feb686a5a2f6c51b66ea9215bfbc4425e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cdr_Index), @"mvc.1.0.view", @"/Views/Cdr/Index.cshtml")]
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
#nullable restore
#line 1 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
using MP.Common;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40822e7feb686a5a2f6c51b66ea9215bfbc4425e", @"/Views/Cdr/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"782901c819350ccc8176209e6f82a108c224e868", @"/Views/_ViewImports.cshtml")]
    public class Views_Cdr_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
#line 2 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
  
    ViewData["Title"] = "Đối soát CDR";
    ViewData["SubTitle"] = "VAS Dealer";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    .dataRegis1Day tr td:nth-child(3), .dataRegis30 tr td:nth-child(3) {
        width: 12% !important;
    }
</style>

<div class=""nav-tabs-custom"">
    <ul class=""nav nav-tabs"">
        <li class=""active"">
            <a href=""#regis30"" data-toggle=""tab""><i class=""fa fa fa-user-plus text-green fa-lg""></i>&nbsp;Đăng ký mới mỗi 30 phút</a>
        </li>
        <li>
            <a href=""#regis1day"" data-toggle=""tab""><i class=""fa fa fa-user text-green fa-lg""></i>&nbsp;Đăng ký mới mỗi ngày</a>
        </li>
        <li>
            <a href=""#renew1day"" data-toggle=""tab""><i class=""fa fa-history text-red fa-lg""></i>&nbsp;Gia hạn mỗi ngày</a>
        </li>
        <li>
            <a href=""#cdrregis"" data-toggle=""tab""><i class=""fa fa-area-chart text-green fa-lg""></i>&nbsp;CDR mời gói</a>
        </li>
        <li>
            <a href=""#cdrrenew"" data-toggle=""tab""><i class=""fa fa-bar-chart text-red fa-lg""></i>&nbsp;CDR gia hạn</a>
        </li>
        <li class=""pull-right header"">");
            WriteLiteral(@"<i class=""fa fa-commenting-o""></i>Thông tin đối soát CDR</li>
    </ul>
    <div class=""tab-content"">
        <div class=""tab-pane active"" id=""regis30"">
            <div class=""row"">
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Từ ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm fromDateRegis30 input-datepicker"" placeholder=""dd/MM/yyyy"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 1694, "\"", 1747, 1);
#nullable restore
#line 39 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 1702, DateTime.Now.ToString(MPFormat.DateTime_103), 1702, 45, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Đến ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm toDateRegis30 input-datepicker"" placeholder=""dd/MM/yyyy"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 2129, "\"", 2182, 1);
#nullable restore
#line 45 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 2137, DateTime.Now.ToString(MPFormat.DateTime_103), 2137, 45, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã đại lý</label>
                        <input class=""form-control input-sm"" id=""txtBranchRegis30"" placeholder=""Mã đại lý"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 2520, "\"", 2528, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã user</label>
                        <input class=""form-control input-sm"" id=""txtUserRegis30"" placeholder=""Mã user"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 2860, "\"", 2868, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-12"">
                    <div class=""form-group"">
                        <button type=""button"" class=""btn btn-flat btn-sm btn-success searchRegis30"">
                            <i class=""fa fa-search""></i> Tìm kiếm
                        </button>
                        <button type=""button"" class=""btn bg-blue-gradient btn-flat btn-sm excelRegis30""><i class=""fa fa-cloud-download""></i>&nbsp;Xuất excel</button>
                    </div>
                </div>
            </div>
            <div class=""box box-primary"">
                <div class=""box-header table-responsive"">
                    <h3 class=""box-title""><i class=""fa fa-clipboard""></i>&nbsp;Kết quả tìm kiếm</h3>
                    <div class=""box-tools pull-right mp-pointer-st"" title=""Chú thích"">
                        <i class=""fa fa-question-circle fa-lg text-info"" data-type=""regis30""></i>
                    </div>
                    ");
            WriteLiteral(@"<table class=""table table-bordered dataRegis30"" style=""width:100%"">
                        <thead>
                            <tr role=""row"">
                                <th>#</th>
                                <th>Mã giao dịch</th>
                                <th>Đại lý</th>
                                <th>Dịch vụ</th>
                                <th>UserName</th>
                                <th>Họ tên</th>
                                <th>Giá gói</th>
                                <th>Thuê bao</th>
                                <th>Giá thực trừ</th>
                                <th>Thời gian đăng ký</th>
                                <th>Trạng thái</th>
                                <th style=""width:150px"">Lỗi</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class=""tab-pane"" id=""regis1day""");
            WriteLiteral(@">
            <div class=""row"">
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Từ ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm fromDateRegis1Day input-datepicker"" placeholder=""dd/MM/yyyy"" type=""text""");
            BeginWriteAttribute("value", "\r\n                               value=\"", 5280, "\"", 5377, 1);
#nullable restore
#line 103 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 5320, DateTime.Now.AddDays(-1).ToString(MPFormat.DateTime_103), 5320, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Đến ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm toDateRegis1Day input-datepicker"" placeholder=""dd/MM/yyyy"" type=""text""");
            BeginWriteAttribute("value", "\r\n                               value=\"", 5761, "\"", 5846, 1);
#nullable restore
#line 110 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 5801, DateTime.Now.ToString(MPFormat.DateTime_103), 5801, 45, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã đại lý</label>
                        <input class=""form-control input-sm"" id=""txtBranchRegis1Day"" placeholder=""Mã đại lý"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 6186, "\"", 6194, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã user</label>
                        <input class=""form-control input-sm"" id=""txtUserRegis1Day"" placeholder=""Mã user"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 6528, "\"", 6536, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-12"">
                    <div class=""form-group"">
                        <button type=""button"" class=""btn btn-flat btn-sm btn-success searchRegis1Day"">
                            <i class=""fa fa-search""></i> Tìm kiếm
                        </button>
                        <button type=""button"" class=""btn bg-blue-gradient btn-flat btn-sm excelRegis1Day""><i class=""fa fa-cloud-download""></i>&nbsp;Xuất excel</button>

                    </div>
                </div>
            </div>
            <div class=""box box-primary"">
                <div class=""box-header table-responsive"">
                    <h3 class=""box-title""><i class=""fa fa-clipboard""></i>&nbsp;Kết quả tìm kiếm</h3>
                    <div class=""box-tools pull-right mp-pointer-st"" title=""Chú thích"">
                        <i class=""fa fa-question-circle fa-lg text-info"" data-type=""regis1day""></i>
                    </div>
            ");
            WriteLiteral(@"        <table class=""table table-bordered dataRegis1Day"" style=""width:100%"">
                        <thead>
                            <tr role=""row"">
                                <th>#</th>
                                <th>Mã giao dịch</th>
                                <th>Đại lý</th>
                                <th>Dịch vụ</th>
                                <th>Mã User</th>
                                <th>Họ tên</th>
                                <th>Giá gói</th>
                                <th>Thuê bao</th>
                                <th>Giá thực trừ</th>
                                <th>Subs_Type</th>
                                <th>Active_Date</th>
                                <th>Thời gian đăng ký</th>
                                <th>Trạng thái</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </d");
            WriteLiteral(@"iv>
        <div class=""tab-pane"" id=""renew1day"">
            <div class=""row"">
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Từ ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm fromDateRenew1Day input-datepicker"" placeholder=""dd/MM/yyyy""
                               type=""text""");
            BeginWriteAttribute("value", " value=\"", 9029, "\"", 9094, 1);
#nullable restore
#line 170 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 9037, DateTime.Now.AddDays(-1).ToString(MPFormat.DateTime_103), 9037, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Đến ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm toDateRenew1Day input-datepicker"" placeholder=""dd/MM/yyyy""
                               type=""text""");
            BeginWriteAttribute("value", " value=\"", 9510, "\"", 9563, 1);
#nullable restore
#line 177 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 9518, DateTime.Now.ToString(MPFormat.DateTime_103), 9518, 45, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã đại lý</label>
                        <input class=""form-control input-sm"" id=""txtBranchRenew1Day"" placeholder=""Mã đại lý"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 9903, "\"", 9911, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã user</label>
                        <input class=""form-control input-sm"" id=""txtUserRenew1Day"" placeholder=""Mã user"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 10245, "\"", 10253, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-12"">
                    <div class=""form-group"">
                        <button type=""button"" class=""btn btn-flat btn-sm btn-danger searchRenew1Day"">
                            <i class=""fa fa-search""></i> Tìm kiếm
                        </button>
                        <button type=""button"" class=""btn bg-blue-gradient btn-flat btn-sm excelRenew1Day""><i class=""fa fa-cloud-download""></i>&nbsp;Xuất excel</button>
                    </div>
                </div>
            </div>
            <div class=""box box-primary"">
                <div class=""box-header table-responsive"">
                    <h3 class=""box-title""><i class=""fa fa-clipboard""></i>&nbsp;Kết quả tìm kiếm</h3>
                    <div class=""box-tools pull-right mp-pointer-st"" title=""Chú thích"">
                        <i class=""fa fa-question-circle fa-lg text-info"" data-type=""renew1day""></i>
                    </div>
               ");
            WriteLiteral(@"     <table class=""table table-bordered dataRenew1Day"" style=""width:100%"">
                        <thead>
                            <tr role=""row"">
                                <th>#</th>
                                <th>Mã giao dịch</th>
                                <th>Đại lý</th>
                                <th>Dịch vụ</th>
                                <th>Mã User</th>
                                <th>Họ tên</th>
                                <th>Giá thực trừ</th>
                                <th>Ngày đăng ký</th>
                                <th>Ngày gia hạn</th>
                                <th>Subs_Type</th>
                                <th>Active_Date</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class=""tab-pane"" id=""cdrregis"">
            <div class=""row"">
                <div clas");
            WriteLiteral(@"s=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Từ ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm mpfromDateCDRRegis input-datepicker"" placeholder=""dd/MM/yyyy""
                               type=""text""");
            BeginWriteAttribute("value", " value=\"", 12639, "\"", 12704, 1);
#nullable restore
#line 234 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 12647, DateTime.Now.AddDays(-1).ToString(MPFormat.DateTime_103), 12647, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Đến ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm mptoDateCDRRegis input-datepicker"" placeholder=""dd/MM/yyyy""
                               type=""text""");
            BeginWriteAttribute("value", " value=\"", 13121, "\"", 13186, 1);
#nullable restore
#line 241 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 13129, DateTime.Now.AddDays(-1).ToString(MPFormat.DateTime_103), 13129, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã user</label>
                        <input class=""form-control input-sm"" id=""txtUserCDRRegis"" placeholder=""Mã user"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 13519, "\"", 13527, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã đại lý</label>
                        <input class=""form-control input-sm"" id=""txtBranchCDRRegis"" placeholder=""Mã đại lý"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 13866, "\"", 13874, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-12"">
                    <div class=""form-group"">
                        <button type=""button"" class=""btn btn-flat btn-sm btn-success searchCDRRegis"">
                            <i class=""fa fa-search""></i> Check CDR mời gói
                        </button>
                        <button type=""button"" class=""btn bg-blue-gradient btn-flat btn-sm excelCDRRegis""><i class=""fa fa-cloud-download""></i>&nbsp;Xuất excel</button>
                    </div>
                </div>
            </div>
            <div class=""box box-primary"">
                <div class=""box-header table-responsive"">
                    <h3 class=""box-title""><i class=""fa fa-clipboard""></i>&nbsp;Kết quả CDR mời gói</h3>
                    <div class=""box-tools pull-right mp-pointer-st"" title=""Chú thích"">
                        <i class=""fa fa-question-circle fa-lg text-info"" data-type=""cdrregis""></i>
                    </div>
     ");
            WriteLiteral(@"               <table class=""table table-bordered CDRRegis"" style=""width:100%"">
                        <thead>
                            <tr role=""row"">
                                <th rowspan=""2"" class=""vertical-middle"">#</th>
                                <th colspan=""6"" class=""text-center"">Minh phúc</th>
                                <th colspan=""7"" class=""text-center"">VPNT</th>
                            </tr>
                            <tr>
                                <th class=""text-center"">Mã giao dịch</th>
                                <th class=""text-center"">Thuê bao</th>
                                <th class=""text-center"">Đại lý</th>
                                <th class=""text-center"">Dịch vụ</th>
                                <th>Tài khoản mời</th>
                                <th>Ngày mời</th>
                                <th class=""text-center"">Mã giao dịch</th>
                                <th class=""text-center"">Thuê bao</th>
               ");
            WriteLiteral(@"                 <th class=""text-center"">Đại lý</th>
                                <th class=""text-center"">Dịch vụ</th>
                                <th class=""text-center"">Giá gói</th>
                                <th class=""text-center"">Giá thực trừ</th>
                                <th>Ngày đăng ký</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class=""tab-pane"" id=""cdrrenew"">
            <div class=""row"">
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Từ ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm fromDateCDRRenew input-datepicker"" placeholder=""dd/MM/yyyy""
                               type=""text""");
            BeginWriteAttribute("value", " value=\"", 16884, "\"", 16949, 1);
#nullable restore
#line 305 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 16892, DateTime.Now.AddDays(-1).ToString(MPFormat.DateTime_103), 16892, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Đến ngày <span class=""text-danger"">*</span></label>
                        <input class=""form-control input-sm toDateCDRRenew input-datepicker"" placeholder=""dd/MM/yyyy""
                               type=""text""");
            BeginWriteAttribute("value", " value=\"", 17364, "\"", 17417, 1);
#nullable restore
#line 312 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
WriteAttributeValue("", 17372, DateTime.Now.ToString(MPFormat.DateTime_103), 17372, 45, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã user</label>
                        <input class=""form-control input-sm"" id=""txtUserCDRRenew"" placeholder=""Mã user"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 17750, "\"", 17758, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-3 col-lg-2 col-sm-6 col-xs-12"">
                    <div class=""form-group"">
                        <label>Mã đại lý</label>
                        <input class=""form-control input-sm"" id=""txtBranchCDRRenew"" placeholder=""Mã đại lý"" type=""text""");
            BeginWriteAttribute("value", " value=\"", 18097, "\"", 18105, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    </div>
                </div>
                <div class=""col-md-12"">
                    <div class=""form-group"">
                        <button type=""button"" class=""btn btn-flat btn-sm btn-danger searchCDRRenew"">
                            <i class=""fa fa-search""></i> Check CDR gia hạn
                        </button>
                        <button type=""button"" class=""btn bg-blue-gradient btn-flat btn-sm excelCDRRenew""><i class=""fa fa-cloud-download""></i>&nbsp;Xuất excel</button>

                    </div>
                </div>
            </div>
            <div class=""box box-primary"">
                <div class=""box-header table-responsive"">
                    <h3 class=""box-title""><i class=""fa fa-clipboard""></i>&nbsp;Kết quả CDR gia hạn</h3>
                    <div class=""box-tools pull-right mp-pointer-st"" title=""Chú thích"">
                        <i class=""fa fa-question-circle fa-lg text-info"" data-type=""cdrrenew""></i>
                    </div>
    ");
            WriteLiteral(@"                <table class=""table table-bordered CDRRenew"" style=""width:100%"">
                        <thead>
                            <tr role=""row"">
                                <th>#</th>
                                <th>Mã giao dịch</th>
                                <th>Đại lý</th>
                                <th>Dịch vụ</th>
                                <th>Thuê bao</th>
                                <th>Phí</th>
                                <th>Ngày gia hạn</th>
                                <th>Ngày đăng ký</th>
                                <th>SubsType</th>
                                <th>Ngày kích hoạt</th>
                                <th>Người tạo</th>
                                <th>Thời gian</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "40822e7feb686a5a2f6c51b66ea9215bfbc4425e28753", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 20142, "~/modules/cdr/cdr.js?v=", 20142, 23, true);
#nullable restore
#line 368 "F:\Project\Vas_Dealer\CRM\Views\Cdr\Index.cshtml"
AddHtmlAttributeValue("", 20165, GetType().Assembly.GetName().Version.ToString(), 20165, 48, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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