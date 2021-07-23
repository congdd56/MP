$(function () {
    //var myVOCDeadLineNotifi;
    //$.support.cors = true;
    //var notifications = $.connection.notificationHub;
    //var inforGuidNeedToTake;
    //var options = {
    //    timeOut: 0,
    //    extendedTimeOut: 0,
    //    tapToDismiss: false,
    //    positionClass: "toast-top-right major-guid-va12",
    //    closeButton: true,
    //};
    //$.connection.hub.start().done(function (e) {
    //}).fail(function (e) {
    //    alert(e);
    //});
});
function BindTransferNotification() {
    var num = 0;

    try {
        $.ajax({
            type: "POST",
            url: "/api/Transfer/GetInfoTicketTransfer",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.value.status === 'ok') {
                    var item = msg.value.value;
                    $('.notify-deadline-number').html(Number(num))
                    for (var i = 0; i < item.length; i++) {
                        num = $('.notify-deadline-number').html();
                        $('.notify-deadline-number').html(Number(num) + 1);
                        $('.notify-deadline-number-member').html('<code>Bạn có ' + (Number(num) + 1) + ' thông báo!</code>');
                        if (typeof item[i] !== 'undefined') {
                            var html = "<li class='li-notifi-inside' ticket-id='" + item[i].TicketId + "' ticket-by='" + item[i].Sender + "' ticket-status='Bàn giao sự vụ'>";
                            html += "<a target='_blank' href='/VOC/CreateTicket?ticketid=" + item[i].TicketId + "'>";
                            html += "<div class='pull-left'>";

                            html += "<img src='/Content/dist/images/Transfer.png' class='img-circle' alt=''";
                            html += "data-toggle='tooltip' data-placement='right' title='Bàn giao sự vụ'>";
                            html += "</div><h4>" + item[i].Sender;
                            html += "<small><i class='fa fa-clock-o'></i> " + item[i].CreatedDate.substring(0, item[i].CreatedDate.length - 3) + "</small>";
                            html += "</h4><p>Mã sự vụ: " + item[i].TicketId + "</p><p>" + item[i].Note + "</p></a></li>";
                            $('#ul-deadline-ticket').prepend(html);
                        };
                    }

                }
            },
            complete: function () {
            }
        });

    } catch (e) {
        console.log(e);
    }
}

function RemoveDupTicketNotifies(names) {
    var uniqueNames = [];
    $.each(names, function (i, el) {
        if ($.inArray(el, uniqueNames) === -1) uniqueNames.push(el);
    });
    return uniqueNames;
}

function createNotification(ticketBy, ticketID) {
    if (!("Notification" in window)) {
        console.log("This browser does not support desktop notification");
    } else if (Notification.permission === "granted") {
        var i = 0;
        var interval = window.setInterval(function () {
            var notification = new Notification(ticketBy, {
                icon: '/Content/dist/images/notification-icon-voc.jpg',
                body: 'Vừa đóng sự vụ: ' + ticketID,
                tag: ticketID
            });
            notification.onclick = function (event) {
                notification.onclick = function () {
                    parent.focus();
                    window.focus();
                    this.close();
                };
            }
            if (i++ == 9) {
                window.clearInterval(interval);
            }
        }, 200);
    } else if (Notification.permission !== "denied") {
        Notification.requestPermission(function (permission) {
            if (permission === "granted") {
                var i = 0;
                var interval = window.setInterval(function () {
                    var notification = new Notification(ticketBy, {
                        icon: '/Content/dist/images/notification-icon-voc.jpg',
                        body: 'Vừa đóng sự vụ: ' + ticketID,
                        tag: ticketID
                    });
                    notification.onclick = function (event) {
                        notification.onclick = function () {
                            parent.focus();
                            window.focus();
                            this.close();
                        };
                    }
                    if (i++ == 9) {
                        window.clearInterval(interval);
                    }
                }, 200);
            }
        });
    }
}

var listItemNotifi; var listItemNotifiService;

function bindNotifi(item) {
    var itemRoleHandler = []; var itemRoleConfirm = []; var itemHasVOCRole = [];

    myVOCDeadLineNotifi = window.setInterval(function () { SetDeadlineNotifi() }, 30000);

    $('.notify-ringing-number').html(item.length);
    $('.notify-ringing-number-member').html('<code>Bạn có ' + item.length + ' thông báo!</code>');

    var itemHasClosedNotifi = [];
    $('li.li-notifi-inside').filter(function () {
        var ticketIdExit = $(this).attr('ticket-id');
        var ticketStatusexit = $(this).attr('ticket-status');
        var ticketBy = $(this).attr('ticket-by');
        var itemNotSame = item.filter(function (p) {
            return p.TicketID === ticketIdExit;
        });
        //Xóa bỏ dòng trên thông báo nếu không tồn tại ở list nhận được
        //trường hợp đóng sự vụ sẽ xóa đi trên server
        if (itemNotSame.length == 0) {
            $(this).remove();
            var obje = new Object();
            obje.ticketBy = ticketBy;
            obje.ticketId = $(this).attr('ticket-id');
            var itemClosedNoti = itemHasClosedNotifi.filter(function (p) {
                return p.ticketId === obje.ticketId;
            });
            if (itemClosedNoti.length == 0) itemHasClosedNotifi.push(obje);
        }
        else {//nếu có tồn tại ở list nhận được, vì ticketid chỉ có 1 nên lấy phần từ đầu tiên
            //Kiểm tra trạng thái nếu có thay đổi thì chỉ cần thay đổi hình ảnh hiển thị trên thông báo.
            if (itemNotSame[0].TicketStatus !== ticketStatusexit) {
                var alink = $(this).children('a');
                var divChildren = $(alink).children('div');
                var imageSource = $(divChildren).children('img');
                if (itemNotSame[0].TicketStatus === 'TTSV-01') {
                    $(imageSource).attr('src', '/Content/dist/images/new-icon.png');
                    $(imageSource).attr('alt', itemNotSame[0].TicketStatus);
                } else if (itemNotSame[0].TicketStatus === 'TTSV-03') {
                    $(imageSource).attr('src', '/Content/dist/images/hanlder-icon.png');
                    $(imageSource).attr('alt', itemNotSame[0].TicketStatus);
                } else if (itemNotSame[0].TicketStatus === 'TTSV-06') {
                    $(imageSource).attr('src', '/Content/dist/images/complete-icon.png');
                    $(imageSource).attr('alt', itemNotSame[0].TicketStatus);
                }
            }
            //xóa bỏ trong list nhận được vì đã xử lý trên giao diện
            item = item.filter(function (p) {
                return p.TicketID !== itemNotSame[0].TicketID;
            });
        }
    });
    for (var i = 0; i < itemHasClosedNotifi.length; i++) {
        createNotification(itemHasClosedNotifi[i].ticketBy, itemHasClosedNotifi[i].ticketId);
    }

    for (var i = 0; i <= item.length; i++) {
        if (typeof item[i] !== 'undefined') {
            var html = "<li class='li-notifi-inside' ticket-id='" + item[i].TicketID + "' ticket-by='" + item[i].TicketBy + "' ticket-status='" + item[i].TicketStatus + "'>";
            html += "<a target='_blank' href='/VOC/CreateTicket?ticketid=" + item[i].TicketID + "'>";
            html += "<div class='pull-left'>";
            if (item[i].TicketStatus === 'TTSV-01') {
                html += "<img src='/Content/dist/images/new-icon.png' class='img-circle' alt='" + item[i].TicketStatusStr + "'";
                html += "data-toggle='tooltip' data-placement='right' title='Tiếp nhận mới'>";
            } else if (item[i].TicketStatus === 'TTSV-03') {
                html += "<img src='/Content/dist/images/hanlder-icon.png' class='img-circle' alt='" + item[i].TicketStatusStr + "'";
                html += "data-toggle='tooltip' data-placement='right' title='Hoàn thành xử lý'>";
            } else if (item[i].TicketStatus === 'TTSV-06') {
                html += "<img src='/Content/dist/images/complete-icon.png' class='img-circle' alt='" + item[i].TicketStatusStr + "'";
                html += "data-toggle='tooltip' data-placement='right' title='Hoàn thành xác nhận khách hàng'>";
            }
            html += "</div><h4>" + item[i].TicketBy;
            var ticketDate = formatDate(new Date(item[i].TicketTime), 'dd/MM/yyyy hh:mm');
            html += "<small><i class='fa fa-clock-o'></i> " + ticketDate + "</small>";
            html += "</h4><p>Mã sự vụ: " + item[i].TicketID + "</p><p>" + item[i].TicketTitle + "</p></a></li>";
            $('#ul-notify-ticket').prepend(html);
        };
    }
}

function bindOneNotifyRinging(item) {
    var num = $('.notify-ringing-number').html();
    $('.notify-ringing-number').html(Number(num) + 1);
    $('.notify-ringing-number-member').html('<code>Bạn có ' + Number(num) + 1 + ' thông báo!</code>');

    var html = "<li class='li-notifi-inside' ticket-id='" + item.TicketID + "' ticket-by='" + item.TicketBy + "' ticket-status='" + item.TicketStatus + "'>";
    html += "<a target='_blank' href='/VOC/CreateTicket?ticketid=" + item.TicketID + "'>";
    html += "<div class='pull-left'>";
    if (item.TicketStatus === 'TTSV-01') {
        html += "<img src='/Content/dist/images/new-icon.png' class='img-circle' alt='" + item.TicketStatusStr + "'";
        html += "data-toggle='tooltip' data-placement='right' title='Tiếp nhận mới'>";
    } else if (item.TicketStatus === 'TTSV-03') {
        html += "<img src='/Content/dist/images/hanlder-icon.png' class='img-circle' alt='" + item.TicketStatusStr + "'";
        html += "data-toggle='tooltip' data-placement='right' title='Hoàn thành xử lý'>";
    } else if (item.TicketStatus === 'TTSV-06') {
        html += "<img src='/Content/dist/images/complete-icon.png' class='img-circle' alt='" + item.TicketStatusStr + "'";
        html += "data-toggle='tooltip' data-placement='right' title='Hoàn thành xác nhận khách hàng'>";
    }
    html += "</div><h4>" + item.TicketBy;
    var ticketDate = formatDate(new Date(item.TicketTime), 'dd/MM/yyyy hh:mm');
    html += "<small><i class='fa fa-clock-o'></i> " + ticketDate + "</small>";
    html += "</h4><p>Mã sự vụ: " + item.TicketID + "</p><p>" + item.TicketTitle + "</p></a></li>";
    $('#ul-notify-ticket').prepend(html);
}

function removeOneNotifyRinging(item) {
    $('li[ticket-id$="' + item + '"]').remove();
    var num = $('.notify-ringing-number').html();
    $('.notify-ringing-number').html(Number(num) - 1);
    $('.notify-ringing-number-member').html('<code>Bạn có ' + Number(num) - 1 + ' thông báo!</code>');
}

function bindItemNotifiService(item) {
    var previousObject = null;
    $('li.li-notifi-service-inside').filter(function () {
        var IdExit = $(this).attr('notifi-id');
        var itemNotSame = item.filter(function (p) {
            return p.ID !== IdExit;
        });
    });
    for (var i = 0; i <= item.length; i++) {
        if (typeof item[i] !== 'undefined') {
            if (item[i].Type == 'Sent') {
                try {
                    var itemContentSent = JSON.parse('{"' + decodeURI(item[i].SentContent).replace(/"/g, '\\"').replace(/&/g, '","').replace(/=/g, '":"') + '"}');
                    var html = "<li class='li-notifi-service-inside' notifi-id='" + item[i].ID + "'>";
                    html += "<a href='#'>";
                    html += "<div class='pull-left'>";
                    html += "<img src='/Content/dist/images/sending-customer-to-webservice.png' class='img-circle' alt='" + item[i].TicketStatusStr + "'>";
                    html += "</div><h4>Yêu cầu tạo thẻ hội viên";
                    var ticketDate = formatDate(new Date(item[i].CreatedDate), 'hh:mm');
                    html += "<small><i class='fa fa-clock-o'></i> " + ticketDate + "</small>";
                    html += "</h4><p>Số điện thoại: " + itemContentSent.cellPhoneNumber + "</p>";
                    html += "<p>Email: " + itemContentSent.email + "</p></a></li>";
                    $('#ul-notify-service').prepend(html);
                    previousObject = itemContentSent;
                } catch (e) {
                    $('#ul-notify-service').prepend('Có lỗi hiện thị thông báo!\nVui lòng liên hệ Admin.');
                    previousObject = null;
                }
            } else if (item[i].Type === 'Response') {
                try {
                    var itemContentSent = '';
                    var html = '';
                    try {
                        itemContentSent = JSON.parse(item[i].Description.split('=')[1]);
                        html = "<li class='li-notifi-service-inside' notifi-id='" + item[i].ID + "'>";
                        html += "<a href='#'>";
                        html += "<div class='pull-left'>";
                        html += "<img src='/Content/dist/images/notification-services.jpg' class='img-circle' alt='" + item[i].TicketStatusStr + "'>";
                        html += "</div><h4>Kết quả tạo thẻ hội viên";
                        var ticketDate = formatDate(new Date(item[i].CreatedDate), 'hh:mm');
                        html += "<small><i class='fa fa-clock-o'></i> " + ticketDate + "</small>";
                        html += "</h4><p>Số điện thoại: " + previousObject.cellPhoneNumber + "</p>";
                        html += "<p data-toggle='tooltip' data-placement='top' title='responsCode=" + itemContentSent.responsCode + "; messageKey=" + itemContentSent.messageKey + "'>";
                        html += "KQ: Code=" + itemContentSent.responsCode + "; Key=" + itemContentSent.messageKey + "</p></a></li>";
                    } catch (e) {
                        html = "<li class='li-notifi-service-inside' notifi-id='" + item[i].ID + "'>";
                        html += "<a href='#'>";
                        html += "<div class='pull-left'>";
                        html += "<img src='/Content/dist/images/notification-services.jpg' class='img-circle' alt='" + item[i].TicketStatusStr + "'>";
                        html += "</div><h4>Kết quả tạo thẻ hội viên";
                        var ticketDate = formatDate(new Date(item[i].CreatedDate), 'hh:mm');
                        html += "<small><i class='fa fa-clock-o'></i> " + ticketDate + "</small>";
                        html += "</h4><p>Số điện thoại: " + previousObject.cellPhoneNumber + "</p>";
                        html += "<p data-toggle='tooltip' data-placement='top' title='" + item[i].Description + "'>";
                        html += "Kết quả: " + item[i].Description + "</p></a></li>";
                    }
                    $('#ul-notify-service').prepend(html);

                } catch (e) {
                    $('#ul-notify-service').prepend('Có lỗi hiện thị thông báo!\nVui lòng liên hệ Admin.');
                }
            }

        };
    }
}

function SetDeadlineNotifi() {
    var listOutOfDeadLine = [];
    for (var i = 0; i < listItemNotifi.length; i++) {
        cleanObject(listItemNotifi[i]);
        if (listItemNotifi[i].DeadLine !== '') {
            var objDeadLine = new Object();
            objDeadLine.TicketID = listItemNotifi[i].TicketID;
            objDeadLine.TicketTitle = listItemNotifi[i].TicketTitle;
            objDeadLine.DeadLine = listItemNotifi[i].DeadLine;
            objDeadLine.TicketStatus = listItemNotifi[i].TicketStatus;
            listOutOfDeadLine.push(objDeadLine);
        }
    }
    for (var i = 0; i < listOutOfDeadLine.length; i++) {
        var myStartDate = (new Date().getTime() - new Date(parseInt(listOutOfDeadLine[i].DeadLine.substr(6)))) / 60000;
        var lstBeginDeadLine = []; var lstOverDeadLine = [];
        if (myStartDate >= -5 && myStartDate <= 0) {
            listOutOfDeadLine[i].Type = 1;
        } else if (myStartDate > 0) {
            listOutOfDeadLine[i].Type = 0;
        } else {
            listOutOfDeadLine = listOutOfDeadLine.filter(function (el) {
                return el.TicketID !== listOutOfDeadLine[i].TicketID;
            });
        }
    }
    bindDeadLineNotifiToControl(listOutOfDeadLine);
}

function bindDeadLineNotifiToControl(itemsOutOfDeadLine) {
    $('.notify-deadline-number').html(itemsOutOfDeadLine.length);
    $('.notify-deadline-number-member').html('<code>Bạn có ' + itemsOutOfDeadLine.length + ' cảnh báo!</code>');
    $('#ul-deadline-ticket').html('');
    for (var i = 0; i < itemsOutOfDeadLine.length; i++) {
        if (itemsOutOfDeadLine[i].TicketStatus !== 'TTSV-06') {//loại bỏ sự vụ đã hoàn thành
            var htmlDeadLine = '';
            var imageType = ''; var titleNofi = '';
            if (itemsOutOfDeadLine[i].Type == 1) {//sắp đến hạn
                imageType = "/Content/dist/images/begin-of-date-deadline-voc.png";
                if (itemsOutOfDeadLine[i].TicketStatus === 'TTSV-01')
                    titleNofi = 'Sắp hết hạn xử lý';
                else titleNofi = 'Sắp hết hạn xác nhận';
            } else {//quá hạn
                var imageType = '/Content/dist/images/out-of-date-deadline-voc.png';
                if (itemsOutOfDeadLine[i].TicketStatus === 'TTSV-01')
                    titleNofi = 'Quá hạn xử lý';
                else titleNofi = 'Quá hạn xác nhận';
            }
            htmlDeadLine += "<li class='li-deadline-service-inside' notifi-id='" + itemsOutOfDeadLine[i].TicketID + "'";
            htmlDeadLine += "data-toggle='tooltip' data-placement='top' title='" + itemsOutOfDeadLine[i].TicketID + "'>";
            htmlDeadLine += "<a target='_blank' href='/VOC/CreateTicket?ticketid=" + itemsOutOfDeadLine[i].TicketID + "'>";
            htmlDeadLine += "<div class='pull-left'>";
            htmlDeadLine += "<img src='" + imageType + "' class='img-circle' alt='" + itemsOutOfDeadLine[i].TicketID + "'>";
            htmlDeadLine += "</div><h4>" + titleNofi;
            var ticketDate = formatDate(new Date(itemsOutOfDeadLine[i].DeadLine), 'dd/MM/yyyy hh:mm');
            htmlDeadLine += "<small><i class='fa fa-clock-o'></i> " + ticketDate + "</small>";
            htmlDeadLine += "</h4><p>Mã sự vụ: " + itemsOutOfDeadLine[i].TicketID + "</p><p>" + itemsOutOfDeadLine[i].TicketTitle + "</p>";
            htmlDeadLine += "</a></li>";
            $('#ul-deadline-ticket').append(htmlDeadLine);
        }
    }
}












































//$(function () {

//    //var notifications = $.connection.notificationHub;
//    //notifications.client.ImportDataMessage = function (msg) {
//    //    console.log("chào");
//    //    var control = $('#spanImportData');
//    //    if (control) {

//    //        if (msg.value == 0) {//Khởi tạo
//    //            console.log(msg);
//    //            /**
//    //            * Khởi tạo thanh progress vào panel thêm mới dữ liệu
//    //            */
//    //            initProgressBarToConfirm($('#spanImportData'));
//    //            /**
//    //            * Đưa dữ liệu vào progress bar
//    //            */
//    //            bindDatatoProgressbar(msg);
//    //        } else {
//    //            console.log(msg);
//    //            /**
//    //            * Đưa dữ liệu vào progress bar
//    //            */
//    //            bindDatatoProgressbar(msg);
//    //        }
//    //    }
//    //};

//    //$.connection.hub.start({ jsonp: true }).done(function () {
//    //}).fail(function (e) {
//    //    alert(e);
//    //});







//    var notifications = $.connection.notificationHub;
//    if (token) {
//        //Thông báo cho việc import data
//        notifications.client.ImportDataMessage = function (msg) {
//            try {
//                var control = $('#spanImportData');
//                if (control) {

//                    if (msg.value == 0) {//Khởi tạo
//                        console.log(msg);
//                        /**
//                        * Khởi tạo thanh progress vào panel thêm mới dữ liệu
//                        */
//                        initProgressBarToConfirm($('#spanImportData'));
//                        /**
//                        * Đưa dữ liệu vào progress bar
//                        */
//                        bindDatatoProgressbar(msg);
//                    } else {
//                        console.log(msg);
//                        /**
//                        * Đưa dữ liệu vào progress bar
//                        */
//                        bindDatatoProgressbar(msg);
//                    }
//                }
//            } catch (e) {
//                console.log(e);
//            }
//        };
//        $.connection.hub.start().done(function (e) {
//        }).fail(function (e) {
//            alert(e);
//        });
//    }
//});

///**
// * Khởi tạo thanh progress vào panel 
// * @param {any} $panel item
// */
//function initProgressBarToConfirm($panel) {
//    var htmControl = '<div class="jconfirm-buttons progress-panel" style="float: left;margin-left: 15px;">';
//    htmControl += '<div class="progress-group" style="min-width: 400px;">';
//    htmControl += '<span class="progress-text"></span>';
//    htmControl += '<span class="progress-number"></span><div class="progress sm">';
//    htmControl += '<div class="progress-bar progress-bar-aqua" style="width: 0%"></div>';
//    htmControl += '</div></div></div>';
//    $panel.append(htmControl);
//    $('.jconfirm-clear').remove();
//}
///**
// * Đưa dữ liệu vào progress bar
// * @param {any} item item
// */
//function bindDatatoProgressbar(item) {
//    $('div.progress-panel  span.progress-text').html(item.text);
//    $('div.progress-panel  span.progress-number').html('<i class="fa fa-spin fa-spinner text-primary"></i> ' + item.value + '%');
//    $('div.progress-panel  div.progress-bar').css('width', item.value + '%');
//}