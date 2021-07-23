const pond4 = FilePond.create(
    document.querySelector('input[name="filepond-kt-tt-kt"]'), {
    labelIdle: '<span>Chưa có phiếu..</span>',
    disabled: true,
    imagePreviewHeight: 80,
    allowRevert: false,
    allowDownloadByUrl: true,
    server: {
        load: '/Receive/LoadDocStation/'
    }
});

const pond5 = FilePond.create(
    document.querySelector('input[name="filepond-p-sc"]'), {
    labelIdle: '<span>Chưa có phiếu..</span>',
    disabled: true,
    allowRevert: false,
    allowDownloadByUrl: true,
    server: {
        load: '/Receive/LoadDocStation/'
    }
});

const pond6 = FilePond.create(
    document.querySelector('input[name="filepond-dnclkpt"]'), {
    labelIdle: '<span>Chưa có phiếu..</span>',
    disabled: true,
    allowRevert: false,
    allowDownloadByUrl: true,
    server: {
        load: '/Receive/LoadDocStation/'
    }
});


$(function () {
    /**
     * Lấy thông tin GetMPLoadPKTTTKT
     * */
    BindDataGetMPLoadPKTTTKT();
    /**
     * Lấy thông tin GetMPLoadSCLKPT
     * */
    BindDataGetMPLoadSCLKPT();
    /**
     * Lấy thông tin GetMPLoadDNCLKPT
     * */
    BindDataGetMPLoadDNCLKPT();
    /**
     * Sự kiện gửi mail
     * */
    BindSendMailStation();
    /**
     * Sự kiện lưu thông tin ở tab2
     * */
    BindSaveTab2();
    /**
     * Sự kiện lưu thông tin ở tab 3
     * */
    BindSaveTab3();
    /**
     * Sự kiện lưu thông tin ở tab4
     * */
    BindSaveTab4();
});

/**
 * DPLEwarrantyFileUpload/PSCTLK/PSC00012575_DPL.pdf
 * DPLEwarrantyFileUpload/PSCTLK/PSC00012575_ASC.pdf
 * Nếu có tồn tại file định dạng _DPL thì mới hiển thị lên
 * @param {any} items
 */
function CheckExistDPLFormatFile(items) {
    var check = false;
    $.each(items, function (idx, val) {
        if (val.FileName.includes("_DPL.pdf")) {
            check = true;
        }
    });
    return check;
}

/**
 * Lấy thông tin GetMPLoadPKTTTKT
 * */
function BindDataGetMPLoadPKTTTKT() {
    var ticketId = $('#txtRTicketId').val();
    if (!ticketId) return;

    $.ajax({
        type: "GET",
        url: '/receive/GetMPLoadPKTTTKT?ticketId=' + ticketId,
        dataType: "json",
        success: function (msg) {
            console.log({ "GetMPLoadPKTTTKT": msg });
            if (msg && msg.length > 0) {
                $('#txtS1DocCode').val(msg[0].NumSeqPKTTTKT);
                $('#txtS1DocDate').val(msg[0].TransDateCreateStr);
                $('#ddlS1Status').val(msg[0].Status).trigger('change.select2');
                if (msg[0].Status === '1' || msg[0].Status === '3') {
                    $('div.panel-station-sendmail').show();
                } else {
                    $('div.panel-station-sendmail').hide();
                }


                //Chỉ load file khi tồn tại định dạng DPL
                if (CheckExistDPLFormatFile(msg)) {
                    pond4.files = [
                        {
                            source: msg[0].NumSeqPKTTTKT + "|PKTTTKT",
                            options: {
                                type: 'local'
                            }
                        }
                    ]
                }
            }
        }
    });
}

/**
 * Lấy thông tin GetMPLoadSCLKPT
 * */
function BindDataGetMPLoadSCLKPT() {
    var ticketId = $('#txtRTicketId').val();
    if (!ticketId) return;

    $.ajax({
        type: "GET",
        url: '/receive/GetMPLoadSCLKPT?ticketId=' + ticketId,
        dataType: "json",
        success: function (msg) {
            console.log({ "GetMPLoadSCLKPT": msg });
            if (msg && msg.length > 0) {
                $('#txtS1DocCodeSCLKPT').val(msg[0].DPL_TTBHPhieuSC);

                //Chỉ load file khi tồn tại định dạng DPL
                if (CheckExistDPLFormatFile(msg)) {
                    pond5.files = [
                        {
                            source: msg[0].DPL_TTBHPhieuSC + "|PSCPTLK",
                            options: {
                                type: 'local'
                            }
                        }
                    ]
                }
            }
        }
    });
}

/**
 * Lấy thông tin GetMPLoadDNCLKPT
 * */
function BindDataGetMPLoadDNCLKPT() {
    var ticketId = $('#txtRTicketId').val();
    if (!ticketId) return;

    $.ajax({
        type: "GET",
        url: '/receive/GetMPLoadDNCLKPT?ticketId=' + ticketId,
        dataType: "json",
        success: function (msg) {
            console.log({ "GetMPLoadDNCLKPT": msg });
            if (msg && msg.length > 0) {
                $('#txtS1DNPTLK').val(msg[0].NumSeqPDNCLKPT);

                $('#txtHCreateRepair_NumberTranfer').val(msg[0].DeliveryNumber);
                $('#txtHCreateRepair_TypeTranfer').val(msg[0].Transport);
                $('#txtHTranRec_DateTransport').val(msg[0].TransDateReceivedPTLKStr);
                $('#txtHTranRec_DateReceiverStationLK').val(msg[0].TransDateRecivedTBHStr);
                $('#txtHTranRec_StatusOrder').val(msg[0].DPL_DeliveryStatus);

                //load file
                pond6.files = [
                    {
                        source: msg[0].NumSeqPDNCLKPT + "|PDNCPTLK",
                        options: {
                            type: 'local'
                        }
                    }
                ]
            }
        }
    });
}

/**
 * Sự kiện gửi mail
 * */
function BindSendMailStation() {
    $('#btnS1SendMail').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var ticketId = $('#txtRTicketId').val();
        if (!ticketId) return;

        var itemDisableds = [];
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
        mylop.start();

        $.confirm({
            title: 'Xác nhận!',
            content: 'Đồng ý gửi email đến trạm?',
            buttons: {
                OK: {
                    text: 'Đồng ý',
                    btnClass: 'btn-blue',
                    keys: ['enter'],
                    action: function () {
                        $.ajax({
                            type: "GET",
                            url: "/receive/SendMailStation/" + ticketId,
                            dataType: "json",
                            success: function (msg) {
                                if (msg.status === 'reload-data') {
                                    location.hash = "#tech";
                                    location.reload();
                                } else {
                                    $.alert({
                                        title: 'Thông báo',
                                        content: 'Gửi mail thành công.',
                                        buttons: {
                                            OK: {
                                                text: 'Đồng ý',
                                                btnClass: 'btn-blue',
                                                action: function () {
                                                    location.hash = "#tech";
                                                    location.reload();
                                                }
                                            }
                                        }
                                    });
                                }
                            },
                            complete: function () {
                                mylop.stop();
                            }
                        });
                    }
                },
                cancel: {
                    text: 'Hủy',
                    btnClass: 'btn-warning',
                    keys: ['esc']
                }
            }
        });
    });
}

/**
 * Sự kiện lưu thông tin ở tab2
 * */
function BindSaveTab2() {
    $('button.btn-save-Tranfer_Receiver').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var item = GetObjectTab2();
        if (!item.TicketId) {
            toastr.warning('Không có thông tin sự vụ', 'Cảnh báo');
            return;
        }
        var itemDisableds = [];
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
        mylop.start();
        $.ajax({
            type: "POST",
            contentType: 'application/json charset=utf-8',
            url: '/receive/SaveTab2Station',
            data: JSON.stringify(item),
            success: function (msg) {
                toastr.info('Lưu thông tin chuyển nhận linh kiện hàng thành công', 'Thông báo');
            },
            complete: function (msg) {
                mylop.stop();
            }
        });
    });
}

/**
 * Lấy object tab2
 * */
function GetObjectTab2() {
    var obj = new Object();
    obj.TicketId = $('#txtRTicketId').val();
    obj.ShippingWithDrawNumber = $('#txtHTranRec_NumberOrderOut').val().trim();
    obj.ReceiveDateStr = $('#txtHTranRec_DateReceiverLK').val().trim();
    obj.WithdrawStatus = $('#txtHTranRec_StatusOrderOut').val().trim();
    return obj;
}

/**
 * Sự kiện lưu thông tin ở tab 3
 * */
function BindSaveTab3() {
    $('button.btn-save-InspectionRepair').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var item = GetObjectTab3();
        if (!item.TicketId) {
            toastr.warning('Không có thông tin sự vụ', 'Cảnh báo');
            return;
        }
        var itemDisableds = [];
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
        mylop.start();
        $.ajax({
            type: "POST",
            contentType: 'application/json charset=utf-8',
            url: '/receive/SaveTab3Station',
            data: JSON.stringify(item),
            success: function (msg) {
                toastr.info('Lưu thông tin sửa chữa kiểm-tra thành công', 'Thông báo');
            },
            complete: function (msg) {
                mylop.stop();
            }
        });
    });
}

/**
 * Lấy object tab 3
 * */
function GetObjectTab3() {
    var obj = new Object();
    obj.TicketId = $('#txtRTicketId').val();
    obj.EnumerationNumber = $('#txtHTranRec_NubmerOrder').val();//Số bảng kê SC-KĐ
    obj.EnumerationDate = $('#txtHTranRec_OrderDate').val().trim();//Ngày lên bảng kê SC-KĐ
    obj.EnumerationCompleteDate = $('#txttHTranRec_DateComplete').val().trim();//Ngày hoàn thành
    return obj;
}

/**
 * Sự kiện lưu thông tin ở tab4
 * */
function BindSaveTab4() {
    $('button.btn-save-HanderFinal').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var item = GetObjectTab4();
        if (!item.TicketId) {
            toastr.warning('Không có thông tin sự vụ', 'Cảnh báo');
            return;
        }
        var itemDisableds = [];
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
        mylop.start();
        $.ajax({
            type: "POST",
            contentType: 'application/json charset=utf-8',
            url: '/receive/SaveTab4Station',
            data: JSON.stringify(item),
            success: function (msg) {
                toastr.info('Lưu thông tin xử lý cuối cùng thành công', 'Thông báo');
            },
            complete: function (msg) {
                mylop.stop();
            }
        });
    });
}

/**
 * Lấy object tab4
 * */
function GetObjectTab4() {
    var obj = new Object();
    obj.TicketId = $('#txtRTicketId').val();
    obj.LastHandlerStatus = $('#ddlHHanderFinal_LastStatus').val();
    obj.HandlerExpectedDate = $('#txtHHanderFinal_ExpectedDate').val().trim();
    obj.HandlerCompleteDate = $('#txtHHanderFinal_CompleteDate').val().trim();
    obj.ReportMonth = $('#txtStatusOrder').val().trim();
    obj.Process = $('#txtHHanderFinal_Timming').val().trim();
    return obj;
}