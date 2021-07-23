$(function () {
    /**
    * Sự kiện tìm kiếm
    * */
    BindEventSearchTicket();
    /**
     * Sự kiện xuất file excel
     * */
    BindEventExport();
});

/**
 * Sự kiện xuất file excel
 * */
function BindEventExport() {
    $('button.btn-export').click(function (e) {
        e.preventDefault();
        var obj = new Object();
        obj.TicketId = $('#txtTicketId').val();
        obj.TicketStatus = $('#ddlSVOCStatus').val();
        obj.CustomerArea = $('#ddlSArea').val();
        obj.Channel = $('#ddlSChannel').val();
        obj.PhoneNumber = $('#txtPhoneNumber').val();
        obj.FromDate = $('#txtFromDate').val();
        obj.ToDate = $('#txtToDate').val();

        var $this = $(this);
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), []);
        mylop.start();
        $.ajax({
            type: "POST",
            url: '/Receive/ExportTicket',
            contentType: 'application/json; charset=utf-8',
            xhrFields: {
                responseType: 'blob' // to avoid binary data being mangled on charset conversion
            },
            data: JSON.stringify(obj),
            success: function (blob, status, xhr) {
                debugger;
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


/**
 * Sự kiện tìm kiếm
 * */
function BindEventSearchTicket() {
    $('button.btn-search').click(function (e) {
        e.preventDefault();
        /**
         * Lấy thông tin tìm kiếm
         * */
        var item = GetSearchObjectTicket($(this));
        /**
         * Lấy kết quả tìm kiếm
         * @param {any} control
         * @param {any} item
         */
        GetSearchResult($(this), item);
    });
}


/**
 * Lấy thông tin tìm kiếm
 * */
function GetSearchObjectTicket(item) {
    var obj = new Object();
    obj.TicketId = $('#txtTicketId').val();
    obj.CustomerArea = $('#ddlSArea').val();
    obj.TicketStatus = $('#ddlSVOCStatus').val();
    obj.Channel = $('#ddlSChannel').val();
    obj.Purpose = $('#ddlSPurpose').val();
    obj.PhoneNumber = $('#txtPhoneNumber').val();
    obj.FromDate = $('#txtFromDate').val().trim();
    obj.ToDate = $('#txtToDate').val().trim();
    return obj;
}


/**
 * Lấy kết quả tìm kiếm
 * @param {any} control
 * @param {any} item
 */
var table;
function GetSearchResult(control, item) {
    var itemDisableds = [];
    var mylop = new myMpLoop(control, 'Đang xử lý', control.html(), itemDisableds);
    mylop.start();
    $.ajax({
        type: "POST",
        url: '/Receive/SearchTicket',
        dataType: "json",
        data: item,
        success: function (msg) {
            $('span.span-total').html(msg.length + ' bản ghi').data('original-title', (msg.length + ' bản ghi'));

            if (table) table.destroy();
            table = $('table#tblList').DataTable({
                "processing": true,
                "searching": false,
                "data": msg,
                "columns": [
                    {
                        "data": "STT",
                        "orderable": false,
                        "searchable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "TicketId",
                        "orderable": false,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<a target="_blank" class="badge bg-yellow" href="/receive/' + data + '#IB">' + data + '</a>';
                            }
                            return data;
                        }
                    },
                    {
                        "data": "NumSeqRetailCM",
                        "orderable": false,
                        "className": 'vertical-middle',
                    },
                    {
                        "data": "RetailCMName",
                        "orderable": false,
                        "className": 'vertical-middle',
                    },
                    {
                        "data": "TicketChannel",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "SEGMENTID_Name",
                        "orderable": false,
                        "className": 'vertical-middle no-wrap'
                    },
                    {
                        "data": "PHONE",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "TicketStatus",
                        "orderable": false,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<span class="badge bg-green">' + data + '</span>';
                            }
                            return data;
                        }
                    },
                    {
                        "data": "CreatedBy",
                        "orderable": false,
                        "className": 'vertical-middle',
                        "render": function (data, type, row, meta) {
                            if (type === 'display') {
                                data = '<span class="no-wrap"><i class="fa fa-user text-blue"></i> <b>' + data + '</b></span><br/>';
                                data += '<span class="no-wrap"><i class="fa fa-calendar text-red"></i> <b>' + row.CreatedDateStr + '</b></span>';
                            }
                            return data;
                        }
                    },
                    {
                        "data": "TakeCareBy",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "CreatedDateCSStr",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "CompletedDateStr",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "SumTotalCS",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Solution",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "CurrentStatus",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Steeps",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "TakeCareContent",
                        "orderable": false,
                        "className": 'vertical-middle'
                    },
                    {
                        "data": "Note",
                        "orderable": false,
                        "className": 'vertical-middle'
                    }
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


function FormatDetailSearch(d) {
    return '<table class="table table-bordered"><thead><tr><th>SĐT gọi đến</th><th>Mã sự vụ</th><th>Tên khách hàng</th>' +
        '<th>SĐT gọi đến</th><th>Kênh tiếp nhận</th><th>Khu vực</th><th>Số điện thoại</th><th>Hiện trạng xử lý</th>' +
        '<th>Tiếp nhận</th><th>Nhân viên chăm sóc</th><th>Bắt đầu CS</th><th>Hoàn thành CS</th><th>Tổng thời gian CS</th><th>Hướng xử lý sau CS</th><th>Hiện trạng</th><th>Các bước chăm sóc</th><th>Nội dung chăm sóc</th> <th>Ghi chú</th>                                                                                                                                                                                                                                                                                                                                                                                                                                                              </th></tr></thead>' +
        '<tbody></tbody></table>';
}