$(function () {
    ShowQuestion();

    BindGetDataRegis30();
    BindDataToGridRegis30();

    BindGetDataRegis1Day();
    BindDataToGridRegis1Day();

    BindGetDataRenew1Day();
    BindDataToGridRenew1Day();

    /**
     * Sự kiện CDRRegis
     * */
    BindGetCDRRegis();
    BindDataCDRRegis();
    BindEventExport();
});


/**
 * Sự kiện Regis30
 * */
function BindGetDataRegis30() {
    $('button.searchRegis30').click(function (e) {
        e.preventDefault();
        var obj = new Object();
        obj.FromDate = $('input.fromDateRegis30').val().trim();
        obj.ToDate = $('input.toDateRegis30').val().trim();
        obj.User = $('#txtUserRegis30').val().trim();
        obj.Branch = $('#txtBranchRegis30').val().trim();
        if (!obj.FromDate || !obj.ToDate) {
            toastr.warning('Vui lòng nhập thời gian tìm kiếm', 'Thông báo');
            return;
        }
        /**
         * Lấy danh sách lưới lịch sử
         * */
        BindDataToGridRegis30();

    });
}
/**
 * Lấy danh sách lưới lịch sử Regis30
 * */
var tableDataRegis30;
function BindDataToGridRegis30() {
    if (tableDataRegis30) {
        tableDataRegis30.clear();
        tableDataRegis30.destroy();
    }
    var itemDisableds = [];
    var mylop = new myMpLoop($('button.searchRegis30'), 'Đang xử lý', $('button.searchRegis30').html(), itemDisableds);
    mylop.start();

    tableDataRegis30 = $('table.dataRegis30').DataTable({
        "proccessing": true,
        "serverSide": true,
        "searching": true,
        "ajax": {
            url: "/cdr/PrefixRegis30",
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.FromDate = $('input.fromDateRegis30').val().trim();
                d.ToDate = $('input.toDateRegis30').val().trim();
                d.User = $("#txtUserRegis30").val().trim();
                d.Branch = $("#txtBranchRegis30").val().trim();
                return JSON.stringify(d);
            },
            complete: function (msg) {
                mylop.stop();
                console.log(msg);
            }
        },
        "language": {
            "search": "",
            "searchPlaceholder": "Tìm kiếm...",
            "emptyTable": "Không có kết quả..<i class='fa fa-heartbeat text-red fa-lg'></i>"
        },
        "columns": [
            {
                "data": "STT",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Trans_Id",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<i class="fa fa-qrcode"></i> <span class="text-info">' + row.Trans_Id + '</span><br/>';

                    }
                    return data;
                }
            },
            {
                "data": "Branch_Code",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<i class="fa fa-building"></i> <span class="text-info">' + row.Branch_Code + '</span>';

                    }
                    return data;
                }
            },
            {
                "data": "Service_Code",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<span class="badge bg-green" title="' + row.Service_Code + '">' + row.Service_Code + '</span>';

                    }
                    return data;
                }
            },
            {
                "data": "UserName",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "FullName",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Price",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<span class="badge bg-red" title="' + numberWithCommas(row.Price) + '">' + numberWithCommas(row.Price) + '</span>';

                    }
                    return data;
                }
            },
            {
                "data": "Subscriber",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Charged_Price",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = numberWithCommas(data);
                    }
                    return data;
                }
            },
            {
                "data": "Regis_DateStr",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
            },
            {
                "data": "Status",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {

                    if (type === 'display') {
                        data = row.Status + '<br/>';
                    }
                    return data;
                }
            },
            {
                "data": "Error_Mess",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {

                    if (type === 'display') {
                        data = '<div class="crop-text-230"><span data-toggle="tooltip" data-placement="left" title="' + row.Error_Mess + '" data-original-title="' + row.Error_Mess + '">' + row.Error_Mess + '</span></div>';
                    }
                    return data;
                }
            }
        ],
        "bLengthChange": false,
        "bInfo": false,
        "iDisplayLength": 10,
        "initComplete": function (settings, json) {
            var api = this.api();
            var textBox = $('#DataTables_Table_0_filter label input');
            textBox.unbind();
            textBox.bind('keyup input', function (e) {
                if (e.keyCode == 8 && !textBox.val() || e.keyCode == 46 && !textBox.val()) {
                } else if (e.keyCode == 13 || !textBox.val()) {
                    api.search(this.value).draw();
                }
            });
        }
    });
}



/**
 * Sự kiện Regis1Day
 * */
function BindGetDataRegis1Day() {
    $('button.searchRegis1Day').click(function (e) {
        e.preventDefault();
        var obj = new Object();
        obj.FromDate = $('input.fromDateRegis1Day').val().trim();
        obj.ToDate = $('input.toDateRegis1Day').val().trim();
        obj.User = $('#txtUserRegis1Day').val().trim();
        obj.Branch = $('#txtBranchRegis1Day').val().trim();
        if (!obj.FromDate || !obj.ToDate) {
            toastr.warning('Vui lòng nhập thời gian tìm kiếm', 'Thông báo');
            return;
        }
        /**
         * Lấy danh sách lưới lịch sử
         * */
        BindDataToGridRegis1Day();

    });
}
/**
 * Lấy danh sách lưới lịch sử Regis1Day
 * */
var tableDataRegis1Day;
function BindDataToGridRegis1Day() {
    if (tableDataRegis1Day) {
        tableDataRegis1Day.clear();
        tableDataRegis1Day.destroy();
    }
    var itemDisableds = [];
    var mylop = new myMpLoop($('button.searchRegis1Day'), 'Đang xử lý', $('button.searchRegis1Day').html(), itemDisableds);
    mylop.start();
    tableDataRegis1Day = $('table.dataRegis1Day').DataTable({
        "proccessing": true,
        "serverSide": true,
        "searching": true,
        "ajax": {
            url: "/cdr/PrefixRegis1Day",
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.FromDate = $('input.fromDateRegis1Day').val().trim();
                d.ToDate = $('input.toDateRegis1Day').val().trim();
                d.Branch = $("#txtBranchRegis1Day").val().trim();
                d.User = $("#txtUserRegis1Day").val().trim();
                return JSON.stringify(d);
            },
            complete: function (msg) {
                mylop.stop();
                console.log(msg);
            }
        },
        "language": {
            "search": "",
            "searchPlaceholder": "Tìm kiếm...",
            "emptyTable": "Không có kết quả..<i class='fa fa-heartbeat text-red fa-lg'></i>"
        },
        "columns": [
            {
                "data": "STT",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Trans_Id",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<i class="fa fa-qrcode"></i> <span class="text-info">' + data + '</span><br/>';

                    }
                    return data;
                }
            },
            {
                "data": "Branch_Code",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<i class="fa fa-building"></i> <span class="text-info">' + row.Branch_Code + '</span>';

                    }
                    return data;
                }
            },
            {
                "data": "Service_Code",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<span class="badge bg-green" title="' + row.Service_Code + '">' + row.Service_Code + '</span>';

                    }
                    return data;
                }
            },
            {
                "data": "UserName",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "FullName",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Price",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<span class="badge bg-red" title="' + numberWithCommas(row.Price) + '">' + numberWithCommas(row.Price) + '</span>';

                    }
                    return data;
                }
            },
            {
                "data": "Subscriber",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {

                "data": "Charged_price",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = numberWithCommas(data);
                    }
                    return data;
                }
            },
            {
                "data": "Subs_Type",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
            },
            {
                "data": "Active_Date",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle',
            },
            {
                "data": "RegisDateStr",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Status",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            }
        ],
        "bLengthChange": false,
        "bInfo": false,
        "iDisplayLength": 10,
        "initComplete": function (settings, json) {
            var api = this.api();
            var textBox = $('#DataTables_Table_1_filter label input');
            textBox.unbind();
            textBox.bind('keyup input', function (e) {
                if (e.keyCode == 8 && !textBox.val() || e.keyCode == 46 && !textBox.val()) {
                } else if (e.keyCode == 13 || !textBox.val()) {
                    api.search(this.value).draw();
                }
            });
        }
    });
}




/**
 * Sự kiện Renew1Day
 * */
function BindGetDataRenew1Day() {
    $('button.searchRenew1Day').click(function (e) {
        e.preventDefault();
        var obj = new Object();
        obj.FromDate = $('input.fromDateRenew1Day').val().trim();
        obj.ToDate = $('input.toDateRenew1Day').val().trim();
        obj.User = $('#txtUserRenew1Day').val().trim();
        obj.Branch = $('#txtBranchRenew1Day').val().trim();
        if (!obj.FromDate || !obj.ToDate) {
            toastr.warning('Vui lòng nhập thời gian tìm kiếm', 'Thông báo');
            return;
        }
        /**
         * Lấy danh sách lưới lịch sử
         * */
        BindDataToGridRenew1Day();

    });
}
/**
 * Lấy danh sách lưới lịch sử Regis1Day
 * */
var tableDataRenew1Day;
function BindDataToGridRenew1Day() {
    if (tableDataRenew1Day) {
        tableDataRenew1Day.clear();
        tableDataRenew1Day.destroy();
    }
    var itemDisableds = [];
    var mylop = new myMpLoop($('button.searchRenew1Day'), 'Đang xử lý', $('button.searchRenew1Day').html(), itemDisableds);
    mylop.start();
    tableDataRenew1Day = $('table.dataRenew1Day').DataTable({
        "proccessing": true,
        "serverSide": true,
        "searching": true,
        "ajax": {
            url: "/cdr/PrefixRenew1Day",
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.FromDate = $('input.fromDateRenew1Day').val().trim();
                d.ToDate = $('input.toDateRenew1Day').val().trim();
                d.Branch = $("#txtBranchRenew1Day").val().trim();
                d.User = $("#txtUserRenew1Day").val().trim();
                return JSON.stringify(d);
            },
            complete: function (msg) {
                mylop.stop();
                console.log(msg);
            }
        },
        "language": {
            "search": "",
            "searchPlaceholder": "Tìm kiếm...",
            "emptyTable": "Không có kết quả..<i class='fa fa-heartbeat text-red fa-lg'></i>"
        },
        "columns": [
            {
                "data": "STT",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Trans_Id",
                "orderable": false,
                "className": 'vertical-middle',
            },
            {
                "data": "Branch_Code",
                "orderable": false,
                "className": 'vertical-middle'
            },
            {
                "data": "Service_Code",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<span class="badge bg-red" title="' + row.Service_Code + '">' + row.Service_Code + '</span>';

                    }
                    return data;
                }
            },
            {
                "data": "UserName",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "FullName",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Charged_Price",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = numberWithCommas(data);

                    }
                    return data;
                }
            },
            {
                "data": "RegisDateStr",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "RenewDateStr",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Subs_Type",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "Active_Date",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            }
        ],
        "bLengthChange": false,
        "bInfo": false,
        "iDisplayLength": 10,
        "initComplete": function (settings, json) {
            var api = this.api();
            var textBox = $('#DataTables_Table_2_filter label input');
            textBox.unbind();
            textBox.bind('keyup input', function (e) {
                if (e.keyCode == 8 && !textBox.val() || e.keyCode == 46 && !textBox.val()) {
                } else if (e.keyCode == 13 || !textBox.val()) {
                    api.search(this.value).draw();
                }
            });
        }
    });
}




/**
 * Sự kiện CDRRegis
 * */
function BindGetCDRRegis() {
    $('button.searchCDRRegis').click(function (e) {
        e.preventDefault();
        var obj = new Object();
        obj.MPFromDate = $('input.mpfromDateCDRRegis').val().trim();
        obj.MPToDate = $('input.mptoDateCDRRegis').val().trim();
        obj.User = $('#txtUserCDRRegis').val().trim();
        obj.Branch = $('#txtBranchCDRRegis').val().trim();
        obj.Type = 1;//Regis

        if (!obj.MPFromDate || !obj.MPToDate) {
            toastr.warning('Vui lòng nhập thời gian MP', 'Thông báo');
            return;
        }

        /**
         * lưới CDR
         * */
        BindDataCDRRegis();

    });
}
/**
 * Lấy danh sách lưới lịch sử Regis1Day
 * */
var tableCDRRegis;
function BindDataCDRRegis() {
    if (tableCDRRegis) {
        tableCDRRegis.clear();
        tableCDRRegis.destroy();
    }
    var itemDisableds = [];
    var mylop = new myMpLoop($('button.searchCDRRegis'), 'Đang xử lý', $('button.searchCDRRegis').html(), itemDisableds);
    mylop.start();
    tableCDRRegis = $('table.CDRRegis').DataTable({
        "proccessing": true,
        "serverSide": true,
        "searching": true,
        "ajax": {
            url: "/cdr/CDRRegis",
            type: 'POST',
            contentType: 'application/json',
            dataFilter: function (msg) {
                msg = JSON.parse(msg);
                console.log(msg);
                for (var i = 0; i < msg.data.length; i++) {
                    msg.data[i] = cleanObject(msg.data[i]);
                }
                return JSON.stringify(msg);
            },
            data: function (d) {
                d.MPFromDate = $('input.mpfromDateCDRRegis').val().trim();
                d.MPToDate = $('input.mptoDateCDRRegis').val().trim();
                //cut user
                var textUser = $('#txtUserCDRRegis').val().trim();
                var arrUser = textUser.split("-");
                var strUser = arrUser[0].trim();
                d.User = strUser;
                //cut Branch
                var textBranch = $('#txtBranchCDRRegis').val().trim();
                var arrBranch = textBranch.split("-");
                var strBranch = arrBranch[0].trim();
                d.Branch = strBranch;
                d.Type = 1;//Regis

                return JSON.stringify(d);
            },
            complete: function (msg) {
                mylop.stop();
                console.log(msg);
            }
        },
        "language": {
            "search": "",
            "searchPlaceholder": "Tìm kiếm...",
            "emptyTable": "Không có kết quả..<i class='fa fa-heartbeat text-red fa-lg'></i>"
        },
        "columns": [
            {
                "data": "STT",
                "orderable": false,
                "searchable": true,
                "className": 'vertical-middle'
            },
            {
                "data": "MP_TradeKey",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.MP_TradeKey + '<br/>';
                    }
                    return data;
                }
            },
            {
                "data": "MP_Phone",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.MP_Phone;

                    }
                    return data;
                }
            },
            {
                "data": "MP_Vendor",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.MP_Vendor + '<br/>';
                    }
                    return data;
                }
            },
            {
                "data": "MP_Service",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = '<span class="badge bg-green" title="' + row.MP_Service + '">' + row.MP_Service + '</span>';
                    }
                    return data;
                }
            },
            {
                "data": "MP_CreatedBy",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.MP_CreatedBy + '<br/>';
                    }
                    return data;
                }
            },
            {
                "data": "MP_CreatedDateStr",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.MP_CreatedDateStr;

                    }
                    return data;
                }
            },
            {
                "data": "VN_Trans_Id",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.VN_Trans_Id + '<br/>';
                    }
                    return data;
                }
            },
            {
                "data": "VN_Subscriber",
                "orderable": false,
                "className": 'vertical-middle',
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.VN_Subscriber;

                    }
                    return data;
                }
            },
            {
                "data": "VN_Branch_Code",
                "orderable": false,
                "searchable": true,
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.VN_Branch_Code + '<br/>';
                    }
                    return data;
                }
            },
            {
                "data": "VN_Service_Code",
                "orderable": false,
                "searchable": true,
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.VN_Service_Code;

                    }
                    return data;
                }
            },
            {
                "data": "VN_Price",
                "orderable": false,
                "searchable": true,
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = numberWithCommas(row.VN_Price) + '<br/>';

                    }
                    return data;
                }
            },
            {
                "data": "VN_Charged_Price",
                "orderable": false,
                "searchable": true,
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = numberWithCommas(row.VN_Charged_Price);
                    }
                    return data;
                }
            },
            {
                "data": "VN_RegisDateStr",
                "orderable": false,
                "searchable": true,
                "render": function (data, type, row, meta) {
                    if (type === 'display') {
                        data = row.VN_RegisDateStr;

                    }
                    return data;
                }
            }
        ],
        "bLengthChange": false,
        "bInfo": false,
        "iDisplayLength": 10,
        "initComplete": function (settings, json) {
            var api = this.api();
            var textBox = $('#DataTables_Table_3_filter label input');
            textBox.unbind();
            textBox.bind('keyup input', function (e) {
                if (e.keyCode == 8 && !textBox.val() || e.keyCode == 46 && !textBox.val()) {
                } else if (e.keyCode == 13 || !textBox.val()) {
                    api.search(this.value).draw();
                }
            });
        }
    });
}





function ShowQuestion() {
    $('i.fa-question-circle').click(function (e) {
        e.preventDefault();
        var dataSchedule;

        var $this = $(this);


        var itemDisableds = [$this];
        var mylop = new myMpLoop($this, '', $this.html(), itemDisableds);
        mylop.start();
        $.ajax({
            type: "GET",
            url: "/cdr/GetSchedule",
            success: function (msg) {
                dataSchedule = msg;
                var data = $this.data('type');
                var contentn = ''; var titlen = '';
                switch (data) {
                    case 'regis30':
                        titlen = 'Chú thích mời gói 30';
                        contentn = '- Dữ liệu mời gói nhà mạng trả về mỗi 30 phút 1 lần.<br/>';
                        contentn += '- Hệ thống tự động chạy lấy dữ liệu vào ' + dataSchedule.Regis30 + ' hoặc khi tìm kiếm sẽ chủ động lấy.<br/>';
                        contentn += '* Dữ liệu sẽ bị chậm 30 phút.';
                        break;
                    case 'regis1day':
                        titlen = 'Chú thích mời gói mỗi ngày';
                        contentn = '- Dữ liệu mời gói ngày hôm trước nhà mạng trả về mỗi ngày vào 12h00.<br/>';
                        contentn += '- Hệ thống tự động chạy lấy dữ liệu vào ' + dataSchedule.Regis1Day + ' hoặc khi tìm kiếm sẽ chủ động lấy.<br/>';
                        contentn += '* Dữ liệu sẽ bị chậm 1,5 ngày.';
                        break;
                    case 'renew1day':
                        titlen = 'Chú thích gia hạn mỗi ngày';
                        contentn += '- Dữ liệu gia hạn ngày hôm trước nhà mạng trả về mỗi ngày vào 12h00.<br/>';
                        contentn += '- Hệ thống tự động chạy lấy dữ liệu vào ' + dataSchedule.Renew1Day + ' hoặc khi tìm kiếm sẽ chủ động lấy.<br/>';
                        contentn += '* Dữ liệu sẽ bị chậm 1,5 ngày.';
                        break;
                    case 'cdrregis':
                        titlen = 'Chú thích đối soát CDR mời gói';
                        contentn += '- Dữ liệu gia hạn ngày hôm trước sẽ được nhà mạng trả về mỗi ngày vào 12h00.<br/>';
                        contentn += '- Tính đối soát theo Mã đại lý, Gói dịch vụ, Số thuê bao.';
                        break;
                    case 'cdrrenew':
                        titlen = 'Chú thích đối soát CDR gia hạn';
                        contentn += '- Dữ liệu gia hạn nhà mạng trả về mỗi ngày vào 12h00.<br/>';
                        contentn += '- Tính đối soát theo Mã đại lý, Gói dịch vụ, Số thuê bao.';
                        break;
                    default:
                }
                console.log(contentn);
                if (contentn) {
                    $.alert({
                        icon: 'fa fa-question-circle',
                        title: titlen,
                        content: contentn
                    });
                }

            },
            complete: function () {
                mylop.stop();
            }
        });




    });
}
function numberWithCommas(x) {
    return x.toString().replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ",");
}

/**
 * Sự kiện xuất file excel
 * */
function BindEventExport() {
    $('button.excelCDRRegis').click(function (e) {
        e.preventDefault();
        var obj = new Object();
        obj.MPFromDate = $('input.mpfromDateCDRRegis').val().trim();
        obj.MPToDate = $('input.mptoDateCDRRegis').val().trim();
        obj.Type = 1;//Regis
        //cut user
        var textUser = $('#txtUserCDRRegis').val().trim();
        var arrUser = textUser.split("-");
        var strUser = arrUser[0].trim();
        obj.User = strUser;
        //cut Branch
        var textBranch = $('#txtBranchCDRRegis').val().trim();
        var arrBranch = textBranch.split("-");
        var strBranch = arrBranch[0].trim();
        obj.Branch = strBranch;
        if (!obj.MPFromDate || !obj.MPToDate) {
            toastr.warning('Vui lòng nhập thời gian MP', 'Thông báo');
            return;
        }
        var $this = $(this);
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), []);
        mylop.start();
        $.ajax({
            type: "POST",
            url: "/cdr/CDRRegisExport",
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
    $('button.excelRegis30').click(function (e) {
        e.preventDefault();
        var obj = new Object();
        obj.FromDate = $('input.fromDateRegis30').val().trim();
        obj.ToDate = $('input.toDateRegis30').val().trim();
        obj.User = $('#txtUserRegis30').val().trim();
        obj.Branch = $('#txtBranchRegis30').val().trim();
        if (!obj.FromDate || !obj.ToDate) {
            toastr.warning('Vui lòng nhập thời gian tìm kiếm', 'Thông báo');
            return;
        }
        var $this = $(this);
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), []);
        mylop.start();
        $.ajax({
            type: "POST",
            url: "/cdr/PrefixRegis30Export",
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
    $('button.excelRegis1Day').click(function (e) {
        e.preventDefault();
        var obj = new Object();
        obj.FromDate = $('input.fromDateRegis1Day').val().trim();
        obj.ToDate = $('input.toDateRegis1Day').val().trim();
        obj.User = $('#txtUserRegis1Day').val().trim();
        obj.Branch = $('#txtBranchRegis1Day').val().trim();
        if (!obj.FromDate || !obj.ToDate) {
            toastr.warning('Vui lòng nhập thời gian tìm kiếm', 'Thông báo');
            return;
        }
        var $this = $(this);
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), []);
        mylop.start();
        $.ajax({
            type: "POST",
            url: "/cdr/PrefixRegis1DayExport",
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
    $('button.excelRenew1Day').click(function (e) {
        e.preventDefault();
        var obj = new Object();
        obj.FromDate = $('input.fromDateRenew1Day').val().trim();
        obj.ToDate = $('input.toDateRenew1Day').val().trim();
        obj.User = $('#txtUserRenew1Day').val().trim();
        obj.Branch = $('#txtBranchRenew1Day').val().trim();
        if (!obj.FromDate || !obj.ToDate) {
            toastr.warning('Vui lòng nhập thời gian tìm kiếm', 'Thông báo');
            return;
        }
        var $this = $(this);
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), []);
        mylop.start();
        $.ajax({
            type: "POST",
            url: "/cdr/PrefixRenew1DayExport",
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
