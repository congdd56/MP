$(function () {
    if (location.hash) {
        $('.nav-tabs a[href="' + location.hash + '"]').tab('show');
    }
    // Replace the <textarea id="editor1"> with a CKEditor
    // instance, using default configuration.
    CKEDITOR.replace('editor1');
    //Sự kiện lưu thông tin
    bindEventSaveInfo();

    /**
     * Lưu nội dung email
     * */
    BindEventSaveContent();

    $('#txtPwKH').change(function (e) {
        e.preventDefault();
        $('input[name="encode"]').val('False');
    });

});

/**
 * Sự kiện lưu thông tin
 * */
function bindEventSaveInfo() {
    $('#btnSave').click(function (e) {
        e.preventDefault();
        var $this = $(this);
        var itemCheck = validateInput();
        if (!itemCheck || !itemCheck.check) return;

        debugger;

        var itemDisableds = [];
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
        mylop.start();


        $.confirm({
            title: 'Xác nhận!',
            content: 'Đồng ý lưu thông tin cấu hình email?',
            buttons: {
                OK: {
                    text: 'Đồng ý',
                    btnClass: 'btn-blue',
                    keys: ['enter'],
                    action: function () {
                        $.ajax({
                            type: "POST",
                            url: "/manager/SaveEmailConfig",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify(itemCheck.Value),
                            success: function (msg) {
                                $.alert({
                                    title: 'Thông báo',
                                    content: 'Lưu thông tin thành công.',
                                    buttons: {
                                        OK: {
                                            text: 'Đồng ý',
                                            btnClass: 'btn-blue',
                                            action: function () {
                                                location.hash = "#config";
                                                location.reload();
                                            }
                                        }
                                    }
                                });
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
                    keys: ['esc'],
                    action: function () {
                    }
                }
            }
        });

    });
}

/**
 * Validate thông tin lưu ở tab 1
 * */
function validateInput() {
    var cf = new Object();
    cf.Host = $('#txtHostKH').val().trim();
    cf.Port = $('#txtPostKH').val().trim();
    cf.UserName = $('#txtEmailKH').val().trim();
    cf.Password = $('#txtPwKH').val().trim();
    cf.Server = $('#txtServerPop').val().trim();
    cf.PortServer = $('#txtPortPop').val().trim();
    cf.SSLoTLS = $('#chkSSLTLSPop').is(':checked');
    cf.Encode = $('input[name="encode"]').val();

    var check = true;
    if (cf.Host === '') {
        $('span.sp-host-email-helper').html('(*)');
        check = false;
    } else {
        $('span.sp-host-email-helper').html('');
    }

    if (cf.Port === '') {
        $('span.sp-port-email-helper').html('(*)');
        check = false;
    } else {
        $('span.sp-port-email-helper').html('');
    }

    if (cf.UserName === '') {
        $('span.sp-email-email-helper').html('(*)');
        check = false;
    } else {
        $('span.sp-email-email-helper').html('');
    }
    if (cf.Password === '') {
        $('span.sp-pw-email-helper').html('(*)');
        check = false;
    } else {
        $('span.sp-pw-email-helper').html('');
    }
    if (cf.Server === '') {
        $('span.sp-serverpop-email-helper').html('(*)');
        check = false;
    } else {
        $('span.sp-serverpop-email-helper').html('');
    }
    if (cf.PortServer === '') {
        $('span.sp-portpop-email-helper').html('(*)');
        check = false;
    } else {
        $('span.sp-portpop-email-helper').html('');
    }

    var rt = new Object();
    rt.check = check;
    rt.Value = cf;
    return rt;
}


/**
 * Lưu nội dung email
 * */
function BindEventSaveContent() {
    $('button.btn-save-mail-content').click(function (e) {
        e.preventDefault();
        var $this = $(this);

        var obj = new Object();
        obj.Id = $this.data('uuid');
        obj.MailSubject = $('#txtMailSubject').val().trim();
        obj.MailContent = CKEDITOR.instances.editor1.getData();

        if (!obj.Id) {
            toastr.warning('Không có thông tin cấu hình email!');
            return;
        }
        if (!obj.MailSubject) {
            toastr.warning('Vui lòng nhập tiêu đề mail');
            return;
        }
        if (!obj.MailContent) {
            toastr.warning('Vui lòng nhập nội dung mail');
            return;
        }

        var itemDisableds = [];
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
        mylop.start();

        $.confirm({
            title: 'Xác nhận!',
            content: 'Đồng ý cập nhật nội dung email?',
            buttons: {
                OK: {
                    text: 'Đồng ý',
                    btnClass: 'btn-blue',
                    keys: ['enter'],
                    action: function () {
                        $.ajax({
                            type: "POST",
                            url: "/manager/SaveEmailContent",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify(obj),
                            success: function (msg) {
                                $.alert({
                                    title: 'Thông báo',
                                    content: 'Lưu thông tin thành công.',
                                    buttons: {
                                        OK: {
                                            text: 'Đồng ý',
                                            btnClass: 'btn-blue',
                                            action: function () {
                                                location.hash = "#content";
                                                location.reload();
                                            }
                                        }
                                    }
                                });
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
                    keys: ['esc'],
                    action: function () {
                    }
                }
            }
        });

    });
}


