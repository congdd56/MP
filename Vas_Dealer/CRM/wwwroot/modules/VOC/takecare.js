$(function () {
    /**
    * Sự kiện lưu thông tin chăm sóc
    * */
    BindEventSaveCSTicket();


});

/**
 * Sự kiện lưu thông tin chăm sóc
 * */
function BindEventSaveCSTicket() {
    $('button.add_info_cs').click(function (e) {
        e.preventDefault();
        /**
         * Lấy thông tin chăm sóc
         * */
        var item = GetCSTicket($(this));
        /**
         * Lưu thông tin chăm sóc
         * @param {any} control
         * @param {any} item
         */
        SaveCS($(this), item);
    });
}


/**
 * Lấy thông tin chăm sóc
 * */
function GetCSTicket(item) {
    var obj = new Object();
    obj.TicketId = $('#txtCTicketId').val();
    obj.TakeCareBy = $('#ddlCTakeCareBy').val();
    obj.Solution = $('#ddlCSolutionAfterCare').val();
    obj.CurrentStatus = $('#ddlCCurrentStatus').val();
    obj.Steeps = $('#ddlCStep').val().join();
    obj.TakeCareContent = $('#txtCTakeCareContent').val().trim();
    obj.Note = $('#txtCNote').val().trim();
    obj.IsClosed = $('#chkCloseTicket').iCheck('update')[0].checked;
    return obj;
}


/**
 * Lưu thông tin chăm sóc
 * @param {any} control
 * @param {any} item
 */
function SaveCS(control, item) {
    var itemDisableds = [];
    var mylop = new myMpLoop(control, 'Đang xử lý', control.html(), itemDisableds);
    mylop.start();
    $.ajax({
        type: "POST",
        url: '/receive/SaveCS',
        dataType: "json",
        data: item,
        success: function (msg) {
            $.confirm({
                title: '<i class="fa fa-question-o text-red"></i> Thông báo',
                content: 'Cập nhật sự vụ thành công.',
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
        },
        complete: function (msg) {
            console.log(msg);
            mylop.stop();
        }
    });
}
