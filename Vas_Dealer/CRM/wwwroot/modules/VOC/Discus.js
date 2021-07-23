$(function () {
    /**
    * Sự kiện lưu thông tin tư vấn
    * */
    BindEventSaveDiscusTicket();


});

/**
 * Sự kiện lưu thông tin tư vấn
 * */
function BindEventSaveDiscusTicket() {
    $('button.add_info_discus').click(function (e) {
        e.preventDefault();
        /**
         * Lấy thông tin tư vấn
         * */
        var item = GetDiscusTicket($(this));
        /**
         * Kiểm tra trước khi lưu thông tin
         * @param {any} item
         */
        var check = ValidateDiscus(item);
        if (!check) {
            toastr.warning("Vui lòng nhập đầy đủ thông tin yêu cầu", 'Cảnh báo');
            return;
        }

        if (!item.TicketId) {
            toastr.warning('Vui lòng chọn sự vụ', 'Cảnh báo');
            return;
        }
        /**
         * Lưu thông tin tư vấn
         * @param {any} control
         * @param {any} item
         */
        SaveDiscus($(this), item);
    });
}

/**
 * Lấy thông tin tư vấn
 * */
function GetDiscusTicket(item) {
    var obj = new Object();
    obj.TicketId = item.data('ticket');
    obj.UserDiscusTransfer = $('#ddlDTransfer').val();
    obj.DiscusContent = $('#txtDContent').val().trim();
    obj.ErrorCategory = $('#ddlDErrorCategory').val();
    obj.Solution = $('#ddlDSolutionAfterDiscus').val();
    return obj;
}


/**
 * Lưu thông tin tư vấn
 * @param {any} control
 * @param {any} item
 */
function SaveDiscus(control, item) {
    var itemDisableds = [];
    var mylop = new myMpLoop(control, 'Đang xử lý', control.html(), itemDisableds);
    mylop.start();
    $.ajax({
        type: "POST",
        url: '/receive/SaveDiscus',
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
                            location.href = "/receive/" + msg.value.Value + '#discus';
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
 * Kiểm tra trước khi lưu thông tin
 * @param {any} item
 */
function ValidateDiscus(item) {
    if (!item.DiscusContent || !item.ErrorCategory || !item.Solution) {
        return false;
    }
}