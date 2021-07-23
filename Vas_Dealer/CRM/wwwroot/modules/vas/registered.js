$(function () {
      
    BindRegis(); 
});

 

/**
 * Tạo mới thuê bao
 * */
function BindRegis() {
    $('button.regis').click(function (e) {
        e.preventDefault();
        var $this = $(this);
         
        $.confirm({
            title: 'Xác nhận',
            content: 'Xác nhận Bạn sẽ quay số may mắn? Lưu ý: Bạn chỉ được quay 1 lần duy nhất!!',
            buttons: {
                OK: {
                    text: 'Xác nhận',
                    btnClass: 'btn-blue',
                    keys: ['enter', 'shift'],
                    action: function () {
                        var itemDisableds = [$this];
                        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
                        mylop.start();

                        $.ajax({
                            type: "POST",
                            url: '/VAS/UpdateLotto',
                            contentType: 'application/json-patch+json',
                            success: function (msg) {
                                debugger; 
                                if (msg == "err-exitsaccount") {
                                    toastr.warning('Admin không được thực hiện việc này!!!.<br/>', 'Thông báo');
                                    return;
                                }
                                if (msg == "err-exitlotto") {
                                    toastr.warning('Bạn đã thực hiện việc quay số may mắn này rồi!!!.<br/>', 'Thông báo');
                                    return;
                                }
                                else {
                                    toastr.success('Bạn đã quay số may mắn thành công. Bạn hãy xem điểm số ở phần bảng xếp hạng<br/>', 'Thông báo');
                                }
                                setTimeout(function () {
                                    location.href = "/vas/hisregis";
                                }, 2000);
                            },
                            complete: function (msg) {
                                mylop.stop();
                            }
                        });
                    }
                },
                cancel: {
                    text: 'Hủy',
                    btnClass: 'btn-warning',
                }
            }
        });




    });
}

 
