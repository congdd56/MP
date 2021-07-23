"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/cCEvents").build();

connection.on("ReceiveMessage", function (message) {
    console.log(message);
});

connection.on("LogOutActice", function (msg) {
    $.confirm({
        title: 'Cảnh báo',
        content: 'Tài khoản đăng nhập của bạn được yêu cầu đăng xuất khỏi hệ thống!',
        type: 'red',
        typeAnimated: true,
        buttons: {
            btn1: {
                text: 'OK',
                btnClass: 'btn-primary',
                action: function () {
                    console.log(msg);
                    location.href = "/Agent/LogOut";
                }
            }
        }
    });

});

/**
 * Tiếp nhận thông tin thay đổi trạng thái agent trong nhóm
 * 
 */
connection.on("ReceiveAgentStatus", function (msg) {
    setTimeout(function () {
        console.log(msg);
        $('div.button-agent-status button').removeAttr('disabled');
        var item = $('a[data-code="' + msg.reason + '"]');
        if (msg.isPause) {
            $('div.button-agent-status button').removeClass().addClass('btn btn-danger dropdown-toggle');
            $('div.button-agent-status button').html('<i class="icon-control-pause"></i> ' + msg.reasonText)
        } else {
            $('div.button-agent-status button').removeClass().addClass('btn btn-info dropdown-toggle');
            $('div.button-agent-status button').html('<i class="icon-check"></i> ' + item.data('name'))
        }
    }, 2000);
});

/***
 * Nhận trạng thái khi thay đổi trạng thái của softphone:
 * -> đang chỉ nhận: Reachable và Unreachable
 * **/
connection.on("ReceivePeerStatus", function (msg) {
    $('.sp-agent-exten-sf-status').html(msg);
});


connection.start().then(function (e) {
    //   alert("hello1");
}).catch(function (err) {
    return console.error(err.toString());
});