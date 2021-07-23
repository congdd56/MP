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
var timer = new easytimer.Timer();
connection.on("RecieveAgentInCall", function (msg) {
    $('div.box__call-info').addClass("d-none");
    var seconds = msg.seconds, minutes = msg.minutes, hours = msg.hours, days = msg.days;
    console.log(msg);
    switch (msg.status) {
        case "0"://Ringing
            {
                timer.reset();
                timer.start({ startValues: [0, seconds, minutes, hours, days] });

                $('.countdown-havecall').remove();
                $('div.agent-have-call').removeClass("d-none");
                $('div.agent-have-call h2').html(msg.phoneNumber);
                $('div.agent-have-call span.caller-name').html(msg.phoneNumber);
                $('div.agent-have-call span.caller-direction').html(msg.direction);
                $('span.btn-have-call').html('Từ chối');
                $('.btn-answer').removeClass('d-none');
                $('span.btn-have-answer').html('Trả lời');
            }
            break;
        case "1"://OnCall
            {
                timer.reset();
                timer.start({ startValues: [0, seconds, minutes, hours, days] });

                $('.countdown-incall').remove();
                $('div.box-have-call').append('<h5 class="text-center countdown-incall"></h5>');
                $('div.agent-have-call').removeClass("d-none");
                $('div.agent-have-call h2').html(msg.phoneNumber);
                $('span.btn-have-call').html('Kết thúc');
                $('div.btn-exchange').removeClass("d-none");
                $('span.btn-call-muted').html('Tắt âm');
                $('span.btn-call-exchange').html('Chuyển CG');
                $('.btn-exchange a').attr('data-channel', msg.channel);
                $('.btn-exchange a').attr('data-context', msg.context);
                $('.btn-muted a').attr('data-channel', msg.channel);
                $('.btn-answer').addClass('d-none');
                var queryUnmuted = document.querySelector('.btn-unmuted.d-none');
                if (queryUnmuted) {
                    $('div.btn-muted').removeClass("d-none");
                } else {
                    $('div.btn-muted').addClass("d-none");
                }
            }
            break;
        case "2"://kết thúc cuộc gọi
            {
                timer.reset();
                $('.countdown-havecall').html('');
                $('.countdown-incall').html('');
                $('.valueTime').html('00:00:00');
                $('div.agent-no-call').removeClass("d-none");
            }
            break;
        default:
            timer.reset();
            $('.countdown-havecall').html('');
            $('.countdown-incall').html('');
            $('.valueTime').html('00:00:00');
            $('div.agent-no-call').removeClass("d-none");
            break;
    }

    timer.addEventListener('secondsUpdated', function (e) {
        $('.valueTime').html(timer.getTimeValues().toString());
    });

    timer.addEventListener('started', function (e) {
        $('.valueTime').html(timer.getTimeValues().toString());
    });

    timer.addEventListener('reset', function (e) {
        $('.valueTime').html(timer.getTimeValues().toString());
    });
});


async function start() {
    try {
        //await connection.start({ transport: 'longPolling' });
        await connection.start();
        console.assert(connection.state === signalR.HubConnectionState.Connected);
    } catch (err) {
        console.assert(connection.state === signalR.HubConnectionState.Disconnected);
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};
connection.onclose(start);

// Start the connection.
start();