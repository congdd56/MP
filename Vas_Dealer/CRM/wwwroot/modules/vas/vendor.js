$(function () {
    $('table.data').DataTable({
        "processing": true,
        "searching": true,
        "language": {
            "search": "",
            "searchPlaceholder": "Tìm kiếm..."
        },
        "bLengthChange": false,
        "bInfo": false,
        "iDisplayLength": 10
    });





    /**
     * Tạo mới đại lý
     * */
    bindCreateVendor();
});

/**
 * Tạo mới đại lý
 * */
function bindCreateVendor() {
    $('button.btn-create').click(function (e) {
        e.preventDefault();
        var $this = $(this);

        var obj = new Object();
        obj.branch_name = $('#txtName').val().trim();
        obj.branch_mobile = $('#txtPhone').val().trim();

        if (!obj.branch_name || !obj.branch_mobile) {
            toastr.warning('Vui lòng nhập đầy đủ thông tin', 'Thông báo');
            return;
        }
        obj.branch_mobile = "84" + obj.branch_mobile;
        var reg = /(84[3|5|7|8|9])+([0-9]{8})\b/g;
        if (!obj.branch_mobile.match(reg)) {
            toastr.warning('Sai định dạng số điện thoại', 'Thông báo');
            return;
        }

        var itemDisableds = [$this];
        var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
        mylop.start();

        $.ajax({
            type: "POST",
            url: '/vas/createvendor',
            contentType: 'application/json-patch+json',
            data: JSON.stringify(obj),
            success: function (msg) {
                toastr.info('Tạo mới đại lý thành công', 'Thông báo');
                setTimeout(function () {
                    location.reload();
                }, 3000);
            },
            complete: function (msg) {
                mylop.stop();
            }
        });

    });
}

//Chỉnh sửa đại lý
$('tbody').on('click', 'span.editVendor', function (e) {
    $(".add").hide();
    var $this = $(this);
    id = $this.attr('uid');
    $.confirm({
        title: '<i class="fa fa-user text-red"></i> Cập nhật đại lý',
        type: 'blue',
        id: 'confirmAddAccount',
        columnClass: 'col-md-8 col-md-offset-2',
        content: 'url:/HtmlModel/VendorModel.html',
        buttons: {
            formSubmit: {
                text: 'Cập nhật',
                btnClass: 'btn-blue btn-add-account',
                action: function () {

                    var obj = new Object();
                    obj.branch_code = id;
                    obj.branch_name = $("input[id$='txtPopName']").val();
                    obj.branch_mobile = $("input[id$='txtPopPhone']").val();

                    if (!obj.branch_name || !obj.branch_mobile) {
                        toastr.warning('Vui lòng nhập đầy đủ thông tin', 'Thông báo');
                        return;
                    }

                    obj.branch_mobile = "84" + obj.branch_mobile;
                    var reg = /(84[3|5|7|8|9])+([0-9]{8})\b/g;
                    if (!obj.branch_mobile.match(reg)) {
                        toastr.warning('Sai định dạng số điện thoại', 'Thông báo');
                        return;
                    }

                    $.ajax({
                        type: "POST",
                        url: "/vas/UpdateVendor",
                        contentType: 'application/json-patch+json',
                        data: JSON.stringify(obj),
                        success: function (msg) {
                            toastr.success("Cập nhật đại lý thành công", "Thông báo");
                            setTimeout(function () {
                                location.href = location.pathname + location.hash;
                                location.reload();
                            }, 3000);
                        }
                    });
                    return false;
                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $.ajax({
                type: "GET",
                url: "/vas/vendorinfo/" + id + "",
                dataType: "json",
                success: function (msg) {
                    console.log(msg);

                    $("input[id$='txtPopName']").val(msg.Name);
                    $("input[id$='txtPopCode']").val(msg.Code);
                    $("input[id$='txtPopPhone']").val(msg.PhoneNo84);
                }
            });

        }
    });

});


//Xóa đại lý
$('tbody').on('click', 'span.deleteVendor', function (e) {
    var id = $(this).attr('uid');
    $.confirm({
        title: 'Xác nhận xóa đại lý',
        content: '' +
            '<form action="" class="formName">' +
            '<div class="form-group">' +
            '<label>Lý do xóa </label>' +
            '<input type="text" placeholder="Nội dung" class="reasion form-control" required />' +
            '</div>' +
            '</form>',
        buttons: {
            formSubmit: {
                text: 'Đồng ý',
                btnClass: 'btn-blue',
                action: function () {
                    var obj = new Object();
                    obj.Reasion = this.$content.find('.reasion').val().trim();
                    obj.BranchCode = id;
                    $.ajax({
                        type: "POST",
                        url: "/vas/deletevendor",
                        contentType: 'application/json-patch+json',
                        data: JSON.stringify(obj),
                        success: function (msg) {
                            toastr.success("Xóa đại lý thành công", "Thông báo");
                            setTimeout(function () {
                                location.href = location.pathname + location.hash;
                                location.reload();
                            }, 3000);
                        }
                    });
                }
            },
            cancel:
            {
                text: 'Hủy'
            },
        }

    });
});