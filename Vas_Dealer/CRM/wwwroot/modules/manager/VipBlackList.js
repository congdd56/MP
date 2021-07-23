


$(function () {

    /***********************Begin: Báo cáo Nguồn nhận*********************/

    /**
    * Sự kiện tìm kiếm
    * */
    BindEventReport1();
    /**
     * Sự kiện xuất file excel
     * */
    BindEventExport();
    /**
    * Sự kiện Lấy danh sách phonetype
    * */
    LoadPhoneType();
    /**
     * Sự kiện Nhập file excel
     * */
    BindEventImportExport();
    /***********************End: Báo cáo Nguồn Nhận*********************/
});


/**
 * Sự kiện tìm kiếm
 * */
function BindEventReport1() {
    $('button.btn-search').click(function (e) {
        e.preventDefault();
        /**
         * Lấy thông tin tìm kiếm
         * */
        var item = GetSearchParams1();
        /**
         * Lấy kết quả tìm kiếm
         * @param {any} control
         * @param {any} item
         */
        GetSearchResult1($(this), item);
    });
}
/**
 * Lấy thông tin tìm kiếm
 * */
function GetSearchParams1() {
    var obj = new Object();
    obj.FromDate = $('#txtFromDate').val();
    obj.ToDate = $('#txtToDate').val();
    if ($("#txtPhoneNumber").val() == null) {
        obj.PhoneNumber = "";
    } else { obj.PhoneNumber = $("#txtPhoneNumber").val(); };
    if ($("#ddList").val() == null) {
        obj.Code = "";
    } else { obj.Code = $("#ddList").val().join(); }
    return obj;
}

/**
 * Lấy kết quả tìm kiếm
 * @param {any} control
 * @param {any} item
 */
var table1;
function GetSearchResult1(control, item) {
    var itemDisableds = [control];
    var mylop = new myMpLoop(control, 'Đang xử lý', control.html(), itemDisableds);
    mylop.start();
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: '/VIPBlackList/GetVipBlacklistResponse',
        dataType: "json",
        data: JSON.stringify(item),
        success: function (msg) {
            //var textEdit = '<i class="fa fa-edit fa-lg text-primary mp-pointer-st pointer editphone" uid="PhoneNumber" alt="delete" title="Cập nhật"></i> <i class="fa far fa-trash fa-lg text-danger mp-pointer-st pointer" alt="delete" title="Xóa thuê bao"></i>';
            if (table1) table1.destroy();
            table1 = $('table#tblList').DataTable({
                "processing": true,
                "searching": false,
                "data": msg,
                "columns": [
                    {
                        "data": "STT",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle text-center'
                    },
                    {
                        "data": "PhoneNumber",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle phone'
                    },
                    {
                        "data": "Code",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle text-center'
                    },
                    {
                        "data": null,
                        "orderable": false,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<i class="fa fa-edit fa-lg text-primary mp-pointer-st pointer editphone" alt="delete" title="Cập nhật"  data-phone="' + row.PhoneNumber + '" data-type="' + row.Code + '"></i> <i class="fa far fa-trash fa-lg text-danger mp-pointer-st pointer deletephone" alt="delete" title="Xóa thuê bao" data-phone="' + row.PhoneNumber + '"></i>';
                            }
                            return data;
                        }
                    },

                ],
                "bLengthChange": false,
                "bInfo": false,
                "iDisplayLength": 10
            });
        },
        complete: function (msg) {
            mylop.stop();
        }
    });
}

/**
 * Sự kiện xuất file excel
 * */
function BindEventExport() {
    $('button.btn-export').click(function (e) {
        e.preventDefault();
        /**
         * Lấy thông tin tìm kiếm
         * */
        var obj = GetSearchParams1();

        var $this = $(this);
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), []);
        mylop.start();
        $.ajax({
            type: "POST",
            url: '/VIPBlackList/ExportVipBlacklistResponse',
            contentType: 'application/json; charset=utf-8',
            xhrFields: {
                responseType: 'blob' // to avoid binary data being mangled on charset conversion
            },
            data: JSON.stringify(obj),
            success: function (blob, status, xhr) {
                // check for a filename
                var filename = "";
                var disposition = xhr.getResponseHeader('Content-Disposition');
                if (disposition && disposition.indexOf('inline') !== -1) {
                    var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                    var matches = filenameRegex.exec(disposition);
                    if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                }

                if (typeof window.navigator.msSaveBlob !== 'undefined') {
                    // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                    window.navigator.msSaveBlob(blob, filename);
                } else {
                    var URL = window.URL || window.webkitURL;
                    var downloadUrl = URL.createObjectURL(blob);

                    if (filename) {
                        // use HTML5 a[download] attribute to specify filename
                        var a = document.createElement("a");
                        // safari doesn't support this yet
                        if (typeof a.download === 'undefined') {
                            window.location.href = downloadUrl;
                        } else {
                            a.href = downloadUrl;
                            a.download = filename;
                            document.body.appendChild(a);
                            a.click();
                        }
                    } else {
                        window.location.href = downloadUrl;
                    }

                    setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
                }
            },
            complete: function (msg) {
                mylop.stop();
            }
        });
    });
}


//Thêm mới Số điện thoại
$('body').on('click', '#btnAddVipBlack', function (e) {
    $.confirm({
        title: '<i class="fa fa-mortar-board text-green"></i>Thêm mới số điện thoại',
        type: 'blue',
        id: 'confirmAddAccount',
        columnClass: 'col-md-6 col-md-offset-3',
        content: 'url:/HtmlModel/CIC/VipBlackList.html',
        buttons: {
            formSubmit: {
                text: 'Thêm mới',
                btnClass: 'btn-blue btn-add-role',
                action: function () {
                    var PhoneNumber = $("#PhoneNumber").val();
                    if (PhoneNumber == "") { toastr.warning("Nhập Số Điện Thoại", "Thông báo"); return false; }
                    var obj = new Object();
                    obj.PhoneNumber = PhoneNumber;
                    if ($("#ddPhoneType").val() == null) {
                        obj.PhoneType = "";
                    } else { obj.PhoneType = $("#ddPhoneType").val(); }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/VIPBlackList/AddPhoneList",
                        //dataType: "json",
                        data: JSON.stringify(obj),
                        success: function (msg) {
                            console.log(msg);
                            toastr.success("Thêm mới thành công", "Thông báo");
                            setTimeout(location.reload.bind(location), 2000);
                            return false;
                        },

                    });
                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('.select2-popup').select2({
                dropdownParent: $('.jconfirm-no-transition')
            });
            $('#txtUserLogin').bind('keyup paste', function () {
                this.value = this.value.replace(/[^0-z_]/g, '');
            });
            $(".add").hide();
            $.ajax({
                type: "POST",
                url: "/VIPBlackList/GetPhoneType",
                contentType: "application/json; charset=utf-8",
                dataType: "json", success: function (msg) {
                    var html = "";
                    var item = msg.listPhoneType;
                    for (var i = 0; i < item.length; i++) {
                        html += "<option value='" + item[i].Code + "'>" + item[i].Name + "</option>";
                    }
                    $("#ddPhoneType").html(html);
                }
            });
        }
    });
});

// Cập Nhật trạng thái thuê bao
$('body').on('click', '.editphone', function (e) {
    var id = "";
    var type = "";
    $(".add").hide();
    $(".update").show();
    $(".pass").hide();
    var $this = $(this);
    id = $this.attr('data-phone');
    type = $this.attr('data-type');
    $.confirm({
        title: '<i class="fa fa-user text-red"></i> Cập nhật Thuê bao',
        type: 'blue',
        id: 'confirmAddAccount',
        columnClass: 'col-md-8 col-md-offset-2',
        content: 'url:/HtmlModel/CIC/VipBlackList.html',
        buttons: {
            formSubmit: {
                text: 'Cập nhật',
                btnClass: 'btn-blue btn-add-account',
                action: function () {
                    var PhoneNumber = $("#PhoneNumber").val();
                    if (PhoneNumber == "") { toastr.warning("Nhập số điện thoại", "Thông báo"); return false; }
                    var obj = new Object();
                    obj.PhoneNumber = PhoneNumber;
                    var PhoneNumber = $this.attr('data-phone');
                    if ((($("#ddPhoneType").val() == null) || ($("#ddPhoneType").val() == ""))) {
                        toastr.warning("Chọn kiểu VIP/Black", "Thông báo"); return false;
                    } else { obj.PhoneType = $("#ddPhoneType").val(); }

                    $.ajax({
                        type: "POST",
                        url: "/VIPBlackList/EditPhonelist",
                        contentType: 'application/json',
                        data: JSON.stringify(obj),
                        success: function (msg) {

                            console.log(msg);
                            toastr.success("Cập nhât thành công", "Thông báo");
                            $("#hidRole").val('');
                            setTimeout(location.reload.bind(location), 2000);
                        },
                        error: function (data) {
                            return false;
                        }
                    });
                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('.select2-popup').select2({
                dropdownParent: $('.jconfirm-no-transition')
            });
            $('#txtUserLogin').bind('keyup paste', function () {
                this.value = this.value.replace(/[^0-z_]/g, '');
            });
            $(".add").hide();
            $.ajax({
                type: "POST",
                url: "/VIPBlackList/GetPhoneType",
                contentType: "application/json; charset=utf-8",
                dataType: "json", success: function (msg) {
                    var html = "";
                    var item = msg.listPhoneType;
                    for (var i = 0; i < item.length; i++) {
                        html += "<option value='" + item[i].Code + "'>" + item[i].Name + "</option>";
                    }
                    $("#ddPhoneType").html(html);
                    $("#ddPhoneType").val(type).trigger("change");
                }
            });
            $("#PhoneNumber").val(id);

            $("#PhoneNumber").prop("disabled", true);
        }
    });

});


//Sự kiện xóa số điện thoại
$('body').on('click', '.deletephone', function (e) {
    var $this = $(this);
    var id = $this.attr('data-phone');
    $.confirm({
        title: 'Xóa Số điện thoại',
        content: 'Bạn có muốn xóa thuê bao này không?',
        buttons: {
            formSubmit: {
                text: 'Đồng ý',
                btnClass: 'btn-blue',
                action: function () {
                    $.ajax({
                        type: "POST",
                        url: "/VIPBlackList/DeletePhonelist?PhoneNumber=" + id + "",
                        //dataType: "json",
                        success: function (msg) {
                            console.log(msg);
                            toastr.success("Xóa số điện thoại thành công", "Thông báo");
                            setTimeout(location.reload.bind(location), 2000);
                            return false;
                        }
                    });
                }
            },
            cancel:
            {
                text: 'Đóng',
                function() {
                },
            },
        }

    });

});


//lấy danh sách PhoneType
function LoadPhoneType() {
    $.ajax({
        type: "POST",
        url: "/VIPBlackList/GetPhoneType",
        contentType: "application/json; charset=utf-8",
        dataType: "json", success: function (msg) {
            var html = "";
            var item = msg.listPhoneType;
            for (var i = 0; i < item.length; i++) {
                html += "<option value='" + item[i].Code + "'>" + item[i].Name + "</option>";
            }
            $("#ddList").html(html);
        }
    });
}


//Lấy file Excel Mẫu
$('button.btn-Exceltemplate').click(function (e) {
    e.preventDefault();
    $.ajax({
        type: "POST",
        url: '/VIPBlackList/Exceltemplate',
        contentType: 'application/json; charset=utf-8',
        xhrFields: {
            responseType: 'blob' // to avoid binary data being mangled on charset conversion
        },
        success: function (blob, status, xhr) {
            if (typeof window.navigator.msSaveBlob !== 'undefined') {
                // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                window.navigator.msSaveBlob(blob, filename);
            } else {
                var URL = window.URL || window.webkitURL;
                var downloadUrl = URL.createObjectURL(blob);
                window.location.href = downloadUrl;
                setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
            }
        },
    });
});
/**
* Sự kiện Nhập file excel
* */
function BindEventImportExport() {
    $('button.btn-upload').on('click', function () {
        var fileExtension = ['xlsx'];
        var filename = $('#fileupload').val();
        if (filename.length == 0) {
            alert("Vui lòng chọn file");
            return false;
        }
        else {
            var extension = filename.replace(/^.*\./, '');
            if ($.inArray(extension, fileExtension) == -1) {
                alert("Chỉ được chọn files excel");
                return false;
            }
        }
        var fdata = new FormData();
        var fileUpload = $("#fileupload").get(0);
        var files = fileUpload.files;
        fdata.append(files[0].name, files[0]);
        $.confirm({
            title: '<i class="fa fa-mortar-board text-green"></i>Các các số nhập thành công',
            type: 'blue',
            id: 'confirmAddAccount',
            columnClass: 'col-md-6 col-md-offset-3',
            content: 'url:/HtmlModel/CIC/ImportVipBlack.html',
            onContentReady: function () {
                $(".jconfirm-content").css("overflow", "hidden");
                $('.select2-popup').select2({
                    dropdownParent: $('.jconfirm-no-transition')
                });
                $('#txtUserLogin').bind('keyup paste', function () {
                    this.value = this.value.replace(/[^0-z_]/g, '');
                });
                $(".add").hide();
                $.ajax({
                    type: "POST",
                    url: "/VIPBlackList/ImportVipBlacklistResponse",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("XSRF-TOKEN",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },
                    data: fdata,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        if (response.length == 0)
                            alert('Some error occured while uploading');
                        else {
                            $('#divPrint').html(response);
                        }
                    },
                    dataType: "json", success: function (msg) {
                        var html = "";
                        var item = msg.value1;
                        for (var i = 0; i < item.length; i++) {
                            html += "<tr><td>" + item[i].PhoneNumber + "</td>";
                            html += "<td>" + item[i].Code + "</td>";
                            html += "</tr>";
                        }
                        $("#tbndata").html(html);
                    }
                });
            }
        });
        $("#fileupload").val("");

    })
}


