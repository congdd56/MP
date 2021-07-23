$(function () {
    //Chỉnh sửa thông tin user
    $("#btn-updateInfo").click(function () {
        $.confirm({
            title: 'Cập nhật thông tin',
            content: 'Xác nhận cập nhật thông tin?',
            buttons: {
                formSubmit: {
                    text: 'Đồng ý',
                    btnClass: 'btn-blue',
                    action: function () {
                        var obj = new Object();
                        obj.UserName = $("input[id$='UserName']").val();
                        obj.FullName = $("input[id$='FullName']").val();
                        obj.PhoneNumber = $("input[id$='PhoneNumber']").val();
                        obj.Email = $("input[id$='Email']").val();
                        obj.Id = $("input[id$='Id']").val();;
                        if (obj.FullName === "") { toastr.info("Nhập họ tên", "Thông báo"); return; }
                        if (obj.UserName === "") { toastr.info("Nhập tên đăng nhập", "Thông báo"); return; }
                        if (obj.PhoneNumber === "") { toastr.info("Nhập số điện thoại", "Thông báo"); return; }
                        if (!ValidateUpdateUser()) return false;
                        $.ajax({
                            type: "POST",
                            url: "/Account/AccountSetting",
                            dataType: "json",
                            data: obj,
                            success: function (msg) {
                                if (msg.value.status == "ok") {
                                    toastr.success("Cập nhật thành công", "Thông báo");
                                }
                                else {
                                    toastr.warning("Tiến trình bị lỗi", "Thông báo");
                                }
                            }
                        });
                    }
                },
                cancel:
                {
                    text: 'Đóng',
                    function() {
                    },
                },
            }

        });
    });

    // Đổi mật khẩu
    $("#btn-changePassword").click(function () {
        $.confirm({
            title: 'Đổi mật khẩu',
            content: 'Xác nhận đổi mật khẩu?',
            buttons: {
                formSubmit: {
                    text: 'Đồng ý',
                    btnClass: 'btn-blue',
                    action: function () {
                        var obj = new Object();
                        obj.passwordNew = $("input[id$='passwordNew']").val();
                        obj.passwordOld = $("input[id$='password']").val();
                        obj.passwodConfirm = $("input[id$='passwordConfirm']").val();
                        obj.Id = $("input[id$='Id']").val();
                        if (obj.passwordOld === "") {
                            toastr.info("Bạn chưa nhập mật khẩu cũ", "Thông báo"); return;
                        }
                        if (obj.passwordNew === "") {
                            toastr.info("Bạn chưa nhập mật khẩu mới", "Thông báo"); return;
                        } else {
                            if (!ValidatePass(obj.passwordNew)) {
                                toastr.info("Mật khẩu phải có ít nhất 6 kí tự gồm ít nhất 1 chữ in,chữ thường và số", "Thông báo");
                                toastr.warning("Sai định dạng trường thông tin" + "Password", "Thông báo");
                                return;
                            }
                        }
                        if (obj.passwodConfirm === "") { toastr.info("Bạn chưa xác nhận mật khẩu", "Thông báo"); return; }
                        if (obj.passwordNew !== obj.passwodConfirm) { toastr.info("Mật khẩu xác nhận không đúng", "Thông báo"); return; }
                        $.ajax({
                            type: "POST",
                            url: "/Account/ChangePassword",
                            dataType: "json",
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify(obj),
                            success: function (msg) {
                                if (msg.value.status == "ok") {
                                    toastr.success("Đổi mật khẩu thành công", "Thông báo");
                                }
                                else if (msg.value.status == "password-failed") {
                                    toastr.warning("Mật khẩu không đúng", "Thông báo");
                                }
                                else {
                                    toastr.warning("Tiến trình bị lỗi", "Thông báo");
                                }

                            }
                        });
                    }
                },
                cancel:
                {
                    text: 'Đóng',
                    function() {
                    },
                },
            }

        });
    });
});

//Chỉnh sửa user
$('tbody').on('click', 'span.editUser', function (e) {
    $(".add").hide();
    $(".update").show();
    $(".pass").hide();
    var $this = $(this);
    id = $this.attr('uid');
    $.confirm({
        title: '<i class="fa fa-user text-red"></i> Cập nhật tài khoản',
        type: 'blue',
        id: 'confirmAddAccount',
        columnClass: 'col-md-8 col-md-offset-2',
        content: 'url:/HtmlModel/AccountModel.html',
        buttons: {
            formSubmit: {
                text: 'Cập nhật',
                btnClass: 'btn-blue btn-add-account',
                action: function () {
                    var obj = new Object();
                    obj.UserName = $("input[id$='txtUserLogin']").val();
                    obj.FullName = $("input[id$='txtFullName']").val(); 
                    obj.IsPriviot = $('#txtCheckbox').is(':checked');
                    obj.Id = id;
                    if (obj.UserName === "") { toastr.info("Nhập tên đăng nhập", "Thông báo"); return; }
                    if (obj.FullName === "") { toastr.info("Nhập họ tên", "Thông báo"); return; }
                      
                    $.ajax({
                        type: "POST",
                        url: "/Account/UpdateAccount",
                        dataType: "json",
                        contentType: 'application/json-patch+json',
                        data: JSON.stringify(obj),
                        success: function (msg) {
                            if (msg.value.status === "exits") {
                                toastr.warning("Tên đăng nhập đã tồn tại", "Thông báo"); return false;
                            }
                            if (msg.value.status === "ok") {
                                toastr.success("Cập nhật thành công", "Thông báo");
                                $("#myModal").modal('hide');
                                location.href = location.pathname + location.hash;
                                location.reload();
                            }
                            else {
                                toastr.warning("Tiến trình bị lỗi", "Cảnh báo");
                            }
                            return false;
                        }, complete: function () {

                        }
                        ,
                        error: function () {
                            toastr.warning("Tiến trình bị lỗi", "Cảnh báo");
                        }
                    });
                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('.select2').select2({
                dropdownParent: $('.jconfirm-no-transition')
            });
            $('#txtUserLogin').bind('keyup paste', function () {
                this.value = this.value.replace(/[^0-z_]/g, '');
            });
            $(".add").hide();
            $.ajax({
                type: "GET",
                url: "/vas/ListVendor",
                dataType: "json",
                success: function (msg) {
                    $('select#ddlVendor').select2({
                        data: msg,
                        dropdownParent: $('div.jconfirm-box-container')
                    });
                    $('select#ddlVendor').on('select2:selecting', function (e) {
                        var itemselect = e.params.args.data.id;
                        if (!itemselect)
                            return false;
                    });


                    $.ajax({
                        type: "POST",
                        url: "/Account/GetInfoAccountById?Id=" + id + "",
                        dataType: "json",
                        success: function (msg) {
                            var item = msg.value.value;
                            var role = msg.value.role;

                            var valueRole = "";
                            for (var i = 0; i < role.length; i++) {
                                valueRole += role[i].RoleId + ",";
                            }

                            $("input[id$='txtUserLogin']").val(item.UserName);
                            $("input[id$='txtEmail']").val(item.Email);
                            $("input[id$='txtPhoneNumber']").val(item.PhoneNumber);
                            $("input[id$='txtFullName']").val(item.FullName);
                            $("select#ddlVendor").val(item.VendorId).trigger("change.select2");
                            if (item.IsPriviot == true) {
                                $("#txtCheckbox").prop('checked', true);
                            }


                            $.ajax({
                                type: "GET",
                                url: "/Account/GetAllRole",
                                dataType: "json", success: function (msg) {
                                    var html = "";
                                    var item = msg.value;
                                    for (var i = 0; i < item.length; i++) {
                                        html += "<option value='" + item[i].Id + "'>" + item[i].Name + "</option>";
                                    }
                                    $("#ddlRole").html(html);
                                    $("#ddlRole").val(valueRole.split(',')).trigger('change');
                                }
                            });
                        }
                    });
                }
            });
        }
    });

});
//Reset mật khẩu
$('tbody').on('click', 'i.resetPassword', function (e) {
    var id = $(this).attr('uid');
    $.confirm({
        title: 'Reset mật khẩu',
        content: 'Xác nhận reset mật khẩu?',
        buttons: {
            formSubmit: {
                text: 'Đồng ý',
                btnClass: 'btn-blue',
                action: function () {
                    $.ajax({
                        type: "POST",
                        url: "/Account/ResetPassword?Id=" + id + "",
                        dataType: "json",
                        success: function (msg) {
                            toastr.success("Reset mật khẩu thành công. Mật khẩu mới là: " + msg.value, "Thông báo");
                        },
                        error: function (msg) {
                            toastr.warning(msg.value, "Thông báo");
                        }
                    });
                }
            },
            cancel:
            {
                text: 'Đóng'
            }
        }
    });
});
//Xóa tài khoản
$('tbody').on('click', 'span.deleteAccount', function (e) {
    var id = $(this).attr('uid');
    $.confirm({
        title: 'Xóa tài khoản',
        content: 'Xác nhận xóa tài khoản?',
        buttons: {
            formSubmit: {
                text: 'Đồng ý',
                btnClass: 'btn-blue',
                action: function () {
                    $.ajax({
                        type: "POST",
                        url: "/Account/DeleteAccount?Id=" + id + "",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.value.status == "ok") {
                                toastr.success("Xóa tài khoản thành công", "Thông báo");
                                location.href = location.pathname + location.hash;
                                location.reload();
                            }
                            else if (msg.value.status == "not-allow") {
                                toastr.warning("Không được phép xóa tài khoản", "Thông báo");
                            }
                            else {
                                toastr.warning("Tiến trình bị lỗi", "Thông báo");
                            }
                        }
                    });
                }
            },
            cancel:
            {
                text: 'Đóng',
                function() {
                },
            },
        }

    });
});
//Khóa tài khoản
$('tbody').on('click', 'i.isLockUser', function (e) {
    e.preventDefault();
    var $this = $(this);
    var id = $this.attr('uid');
    var check = $this.attr('check');
    var mess = "Xác nhận khóa tài khoản";
    if (check === "true") mess = "Xác nhận mở tài khoản";
    $.confirm({
        title: 'Thông báo',
        content: mess,
        buttons: {
            formSubmit: {
                text: 'Đồng ý',
                btnClass: 'btn-blue',
                action: function () {
                    $.ajax({
                        type: "POST",
                        url: "/Account/UnLockAccount?Id=" + id + "&IsLock=" + check + "",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.value.status === "ok") {
                                toastr.success("Cập nhật thành công", "Thông báo");
                                location.href = location.pathname + location.hash;
                                location.reload();
                            }
                            else toastr.warning("Tiến trình bị lỗi.Thử lại!", "Thông báo");
                        }
                    });
                }
            },
            cancel:
            {
                text: 'Đóng',
                function() {
                },
            },
        }

    });






});
var tblUser;
var tblUserLocked;
var id;

$('#tblUser').DataTable({
    "retrieve": true,
    "processing": true,
    "info": false,
    "lengthChange": false,
    language: {
        paginate: { previous: '<', next: '>' },
        search: ''
    }
});



function ValidateAddUser() {
    var html = '';
    if (!validateUsername($("#txtUserLogin").val())) {
        if (!validateUsername($("#txtUserLogin").val())) {
            html += ' "UserName"';
            toastr.info("Tên đăng nhập chỉ được có chữ,số và đấu gạch dưới", "Thông báo");

        }  
        toastr.warning("Sai định dạng trường thông tin" + html, "Thông báo");
        return false;
    } else return true;
}
function ValidateUpdateUser() {
    var html = '';
    if (!validateEmail($("input[id$='Email']").val())) {
        if (!validateEmail($("#txtEmail").val())) {
            html += ' "Email"';
        }
        toastr.warning("Sai định dạng trường thông tin" + html, "Thông báo");
        return false;
    } else return true;
}
function ValidatePass(pass) {
    var re = /(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}/;
    // toastr.info("MK phải có ít nhất 6 kí tự gồm ít nhất 1 chữ in,chữ thường và số", "Thông báo");
    return re.test(pass);
}
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
function validateUsername(username) {
    var re = /^\w+$/;
    //  toastr.info("Tên đăng nhập chỉ được có chữ,số và đấu gạch dưới", "Thông báo"); return false;
    return re.test(username);
}
function validateNumber(Number) {
    var re = /^[+]*[(]{0,1}[0-9]{1,3}[)]{0,1}.{9,}[-\s\./0-9]*$/g;
    return re.test(Number);
}



//Thêm mới tài khoản
$("#btnAddModal").click(function (e) {
    $.confirm({
        title: '<i class="fa fa-bank text-green"></i> Thêm mới tài khoản',
        type: 'blue',
        id: 'confirmAddAccount',
        columnClass: 'col-md-8 col-md-offset-2',
        content: 'url:/HtmlModel/AccountModel.html',
        buttons: {
            formSubmit: {
                text: 'Thêm mới',
                btnClass: 'btn-blue btn-add-account',
                action: function () {
                    var obj = new Object(); 
                    if ($("input[id$='txtFullName']").val() === "") { toastr.info("Nhập họ tên", "Thông báo"); return false; }
                    if ($("input[id$='txtUserLogin']").val() === "") { toastr.info("Nhập tên đăng nhập", "Thông báo"); return false; }
                      
                    if (!ValidateAddUser()) return false;
                    obj.UserName = $("input[id$='txtUserLogin']").val();
                    obj.FullName = $("input[id$='txtFullName']").val();
                    obj.IsActive = $('#chkActive').is(':checked');
                    obj.IsPriviot = $('#txtCheckbox').is(':checked');

                    $.ajax({
                        type: "POST",
                        url: "/Account/AddAccount",
                        dataType: "json",
                        contentType: 'application/json-patch+json',
                        data: JSON.stringify(obj),
                        success: function (msg) {
                            switch (msg.value.status) {
                                case "ok":
                                    toastr.success("Thêm mới thành công", "Thông báo");
                                    location.href = location.pathname + location.hash;
                                    location.reload();
                                    $('#myModal').modal('hide');
                                    break;
                                case "err-exit":
                                    toastr.warning("Tên đăng nhập đã tồn tại", "Thông báo");
                                    return false;
                                    break;
                                default:
                                    toastr.warning("Tiến trình bị lỗi", "Cảnh báo");
                                    return false;
                                    break;
                            }
                            return false;
                        }, complete: function () {
                        }
                        ,
                        error: function () {
                            toastr.warning("Tiến trình bị lỗi", "Cảnh báo");
                        }
                    });
                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc'],
                action: function () {
                }
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('.select2').select2({
                dropdownParent: $('.jconfirm-no-transition')
            });
            $('#txtUserLogin').bind('keyup paste', function () {
                this.value = this.value.replace(/[^0-z_]/g, '');
            });
            GetAllRole();

            $.ajax({
                type: "GET",
                url: "/vas/ListVendor",
                dataType: "json",
                success: function (msg) {
                    $('select#ddlVendor').select2({
                        data: msg,
                        dropdownParent: $('div.jconfirm-box-container')
                    });
                    $('select#ddlVendor').on('select2:selecting', function (e) {
                        var itemselect = e.params.args.data.id;
                        if (!itemselect)
                            return false;
                    });
                }
            });
        }
    });
});
//Thêm mới quyền
$("#btnAddRoleModal").click(function (e) {
    $.confirm({
        title: '<i class="fa fa-mortar-board text-green"></i> Thêm mới quyền',
        type: 'blue',
        id: 'confirmAddAccount',
        columnClass: 'col-md-6 col-md-offset-3',
        content: 'url:/HtmlModel/RoleModel.html',
        buttons: {
            formSubmit: {
                text: 'Thêm mới',
                btnClass: 'btn-blue btn-add-role',
                action: function () {
                    var name = $("#txtNameRoleModal").val();
                    if (name == "") { toastr.warning("Nhập tên nhóm quyền", "Thông báo"); return; }
                    var obj = new Object();
                    obj.Name = name;
                    obj.Permissions = $("input:checkbox:checked").map(function () {
                        return $(this).val();
                    }).get().join();
                    obj.Users = "";
                    obj.Id = 0;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "/Permission/AddRole",
                        dataType: "json",
                        data: JSON.stringify(obj),
                        success: function (msg) {
                            if (msg.status === 'err-exit') {
                                toastr.warning("Mã nhóm quyền đã tồn tại", "Thông báo");
                                return false;
                            }
                            if (msg.status === 'ok') {
                                toastr.success("Thêm mới thành công", "Thông báo");
                                setTimeout(location.reload.bind(location), 2000);
                            } else {
                                toastr.warning("Tiến trình bị lỗi", "Thông báo");
                            }
                            return false;
                        },
                        error: function (data) {
                            toastr.warning("Tiến trình bị lỗi", "Thông báo");
                        }
                    });
                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('.select2').select2({
                dropdownParent: $('.jconfirm-no-transition')
            });
            $('div.pop-manager').hide();
            $.ajax({
                type: "POST",
                url: "/Permission/ListGroupPermission",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var html = "";
                    var itemPer = msg;
                    if (itemPer != null) {
                        for (var i = 0; i < itemPer.length; i++) {
                            html += "<tr>  <td><table class='table table - striped'><thead><tr><th>" + (i + 1) + "</th><th>" + itemPer[i].GroupName + "</th></tr></thead> <tbody>";
                            for (var j = 0; j < itemPer[i].Permissions.length; j++) {
                                var check = itemPer[i].Permissions[j].IsCheck ? "checked" : '';
                                html += " <tr><td><input type='checkbox' " + check + " value=" + itemPer[i].Permissions[j].Id + " id='chkPer" + itemPer[i].Permissions[j].Id + "' value=''></td><td>" + itemPer[i].Permissions[j].Name + "</td></tr>";
                            }
                            html += "</tbody></table></td></tr>";
                        }
                    }
                    $("#tbdGroup1").html(html);
                }
            });
        }
    });
});
//Lấy danh sách nhóm chức năng
function GetAllPermission() {
    $.ajax({
        type: "GET",
        url: "/Role/GetAllPermission",
        dataType: "json", success: function (msg) {
            var html = "";
            var item = msg.value;
            for (var i = 0; i < item.length; i++) {
                html += "<option value='" + item[i].Id + "'>" + item[i].Name + "</option>";
            }
            $("#ddlPermission").html(html);
        }
    });

}
//Lấy danh sách nhóm quyền
function GetAllRole() {
    $.ajax({
        type: "GET",
        url: "/Account/GetAllRole",
        dataType: "json", success: function (msg) {
            var html = "";
            var item = msg.value;
            for (var i = 0; i < item.length; i++) {
                html += "<option value='" + item[i].Id + "'>" + item[i].Name + "</option>";
            }
            $("#ddlRole").html(html);
        }
    });
}

//Hiện thị danh sách user theo nhóm quyền
$('body').on('click', 'td.detailUserByRole', function (e) {
    if ($.fn.dataTable.isDataTable('#tableDetailRole')) {
        $('#tableDetailRole').DataTable().destroy()
    }
    var $this = $(this);
    var itemDisableds = [$this];
    var mylop = new myMpLoop($this, 'Đang xử lý', $this.html(), itemDisableds);
    mylop.start();
    $.ajax({
        type: "POST",
        url: "/Role/GetInfoUserByRoleId?Id=" + $this.attr('uid') + "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var item = msg.value.userByRole;
            var html = "";
            for (var i = 0; i < item.length; i++) {
                html += "<tr><td>" + (i + 1) + "</td>";
                html += "<td>" + item[i].Name + "</td>";
                html += "<td>" + item[i].FullName + "</td>";
                html += "<td>" + item[i].Email + "</td>";
                html += "<td class='text-center'>" + (item[i].Manager == true ? '<i class="fa fa-check-square-o fa-lg text-green"></i>' : '<i class="fa fa-close fa-lg text-red"></i>') + "</td>";
                html += "</tr>";
            }
            $("#tbdDetailUserByRole").html(html);
            $('#tableDetailRole').DataTable({
                "retrieve": true,
                "processing": true,
                "info": false,
                "lengthChange": false,
                language: {
                    paginate: { previous: '<', next: '>' },
                    search: ''
                }
            });
            $(".detail-user-role").show();
        },
        error: function (msg) {
            toastr.warning("Quá trình lấy dữ liệu nhóm quyền bị lỗi", "Thông báo");
        },
        complete: function (msg) {
            mylop.stop();
        }
    });

});
/**
 * Sự kiện bấm chỉnh sửa nhóm quyền
 * **/
$('tbody').on('click', 'span.editRole', function (e) {
    var $this = $(this);
    $("#hidRole").val($this.attr('uid'));
    var itemaddAccount = $.confirm({
        title: '<i class="icon-anchor text-info"></i> Cập nhật quyền',
        type: 'blue',
        id: 'confirmAddAccount',
        columnClass: 'col-md-8 col-md-offset-2',
        content: 'url:/HtmlModel/RoleModel.html',
        buttons: {
            formSubmit: {
                text: 'Cập nhật',
                btnClass: 'btn-blue btn-add-role',
                action: function () {
                    var name = $("#txtNameRoleModal").val();
                    if (name == "") { toastr.warning("Nhập tên nhóm quyền", "Thông báo"); return false; }

                    var obj = new Object();
                    obj.Name = name;
                    var id = $this.attr('uid');
                    obj.Id = id;
                    obj.Permissions = $("input:checkbox:checked").map(function () {
                        return $(this).val();
                    }).get().join();
                    if ($("#ddUser").val() == null) {
                        obj.Users = "";
                    } else { obj.Users = $("#ddUser").val().join(); }
                    $.ajax({
                        type: "POST",
                        url: "/Permission/UpdateRole",
                        dataType: "json",
                        contentType: 'application/json',
                        data: JSON.stringify(obj),
                        success: function (msg) {
                            if (msg.status === 'ok') {
                                toastr.success("Cập nhât thành công", "Thông báo");
                                $("#hidRole").val('');

                                setTimeout(location.reload.bind(location), 2000);
                            } else {
                                toastr.warning("Tiến trình bị lỗi", "Thông báo");
                            }
                            return false;
                        },
                        error: function (data) {
                            toastr.warning("Tiến trình bị lỗi", "Thông báo");
                            return false;
                        },
                        complete: function (msg) {
                        }
                    });

                }
            },
            cancel: {
                text: 'Hủy',
                keys: ['esc']
            }
        },
        onContentReady: function () {
            $(".jconfirm-content").css("overflow", "hidden");
            $('.select2').select2({
                dropdownParent: $('.jconfirm-no-transition')
            });




            $(".add").hide();
            $.ajax({
                type: "POST",
                url: "/Permission/ListGroupPermissionByRole/" + $this.attr('uid'),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var html = "";
                    var itemPer = msg.GroupPermission;
                    for (var i = 0; i < itemPer.length; i++) {
                        html += "<tr>  <td><table class='table table - striped'><thead><tr><th>" + (i + 1) + "</th><th>" + itemPer[i].GroupName + "</th></tr></thead> <tbody>";
                        for (var j = 0; j < itemPer[i].Permissions.length; j++) {
                            var check = itemPer[i].Permissions[j].IsCheck ? "checked" : '';
                            html += " <tr><td><input type='checkbox' " + check + " value=" + itemPer[i].Permissions[j].Id + " id='chkPer" + itemPer[i].Permissions[j].Id + "' value=''></td><td>" + itemPer[i].Permissions[j].Name + "</td></tr>";
                        }
                        html += "</tbody></table></td></tr>";
                    }
                    $("#tbdGroup1").html(html);

                    $("#txtNameRoleModal").val($("#tblRole ." + ($this.attr('uid'))).text());

                }
            });
            $.ajax({
                type: "POST",
                url: "/Role/GetInfoUserByRoleId?Id=" + $this.attr('uid') + "",
                contentType: "application/json; charset=utf-8",
                dataType: "json", success: function (msg) {
                    var html = "";
                    var item = msg.value.userByRole;
                    for (var i = 0; i < item.length; i++) {
                        html += "<option value='" + item[i].Id + "'>" + item[i].Name + "</option>";
                    }
                    $("#ddUser").html(html);
                    var userByRole = msg.value.userByRole;
                    for (var i = 0; i < userByRole.length; i++) {
                        var check = userByRole[i].Manager == false ? "ddUser" : '';
                        $("#ddUser option[" + check + "value='" + userByRole[i].Id + "']").prop("selected", true);
                    }

                }
            });
        }
    });

});
//Sự kiện xóa quyền
$('body').on('click', 'span.deleteRole', function (e) {
    var $this = $(this);
    var id = $this.attr('uid');
    $.confirm({
        title: 'Xóa nhóm',
        content: 'Bạn có muốn xóa nhóm quyền này không?',
        buttons: {
            formSubmit: {
                text: 'Đồng ý',
                btnClass: 'btn-blue',
                action: function () {
                    $.ajax({
                        type: "POST",
                        url: "/Role/DeleteRole?Id=" + id + "",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.value.status == "ok") {
                                toastr.success("Xóa nhóm thành công", "Thông báo");
                                location.href = location.pathname + location.hash;
                                location.reload();
                            }
                            else {
                                toastr.warning("Tiến trình bị lỗi", "Thông báo");
                            }

                        }
                    });
                }
            },
            cancel:
            {
                text: 'Đóng',
                function() {
                },
            },
        }

    });

});


function GetAgent() {
    $.ajax({
        type: "POST",
        url: "/Account/GetListAccount?Name=",
        dataType: "json", success: function (msg) {
            var html = "";
            var item = msg.value;
            for (var i = 0; i < item.length; i++) {
                html += "<option value='" + item[i].Id + "'>" + item[i].UserName + " - " + item[i].FullName + "</option>";
            }
            $("#ddlAgent").html(html);
        }
    });
}
//Ẩn tab chi tiết user khi chuyển tab
$('body').on('click', '.nav-link', function (e) {

    $(".detail-user-role").hide();
});

$('#tblRole').DataTable({
    "retrieve": true,
    "processing": true,
    "info": false,
    "lengthChange": false,
    language: {
        paginate: { previous: '<', next: '>' },
        search: ''
    }
});
