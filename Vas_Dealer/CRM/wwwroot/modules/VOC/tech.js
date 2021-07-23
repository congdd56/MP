$(function () {
    /**
    * Sự kiện lưu thông tin Kỹ thuật
    * */
    BindEventSaveTechTicket();


});

/**
 * Sự kiện lưu thông tin Kỹ thuật
 * */
function BindEventSaveTechTicket() {
    $('button.add_info_tech').click(function (e) {
        e.preventDefault();
        /**
         * Lấy thông tin Kỹ thuật
         * */
        var item = GetTechTicket($(this));
        /**
         * Kiểm tra thông tin 
         * @param {any} item
         */
        if (!ValidateSaveTech(item)) {
            return;
        }
        /**
         * Lưu thông tin Kỹ thuật
         * @param {any} control
         * @param {any} item
         */
        SaveTech($(this), item);
    });
}


/**
 * Lấy thông tin Kỹ thuật
 * */
function GetTechTicket(item) {
    var obj = new Object();
    obj.TicketId = $('#txtTTicketId').val();
    obj.TranTeamleader = $('#ddlTTranTeamleader').val();
    obj.Solution = $('#ddlTSolutionAfterDiscus').val();
    obj.TranEmployee = $('#ddlTTranEmployee').val();
    obj.Station = $('#ddlTStationInfo').val();
    if ($('#chkNewRequestDoc').iCheck('update')[0])
        obj.IsNewRequestDoc = $('#chkNewRequestDoc').iCheck('update')[0].checked;
    else {
        debugger;
        obj.Tickets = $('input[name="chosen-tech-manager"]:checked').map(function () {
            return $(this).data('ticket');
        }).get();
    }
    return obj;
}


/**
 * Lưu thông tin Kỹ thuật
 * @param {any} control
 * @param {any} item
 */
function SaveTech(control, item) {
    var itemDisableds = [];
    var mylop = new myMpLoop(control, 'Đang xử lý', control.html(), itemDisableds);
    mylop.start();
    $.ajax({
        type: "POST",
        url: '/receive/SaveTech',
        dataType: "json",
        data: item,
        success: function (msg) {
            $.confirm({
                title: '<i class="fa fa-question-o text-red"></i> Thông báo',
                content: msg.value.Message,
                type: 'success',
                typeAnimated: true,
                buttons: {
                    tryAgain: {
                        text: 'OK',
                        btnClass: 'btn-success',
                        action: function () {
                            location.href = "/receive/" + msg.value.Value + '#tech';
                            location.reload();
                        }
                    }
                }
            });
            console.log(msg);
        },
        complete: function (msg) {
            console.log(msg);
            mylop.stop();
        }
    });
}

/**
 * Kiểm tra thông tin 
 * @param {any} item
 */
function ValidateSaveTech(item) {
    if (item.IsNewRequestDoc && !item.Station) {
        toastr.warning('Vui lòng chọn thông tin trạm', 'cảnh báo');
        return false;
    }
    return true;
}
